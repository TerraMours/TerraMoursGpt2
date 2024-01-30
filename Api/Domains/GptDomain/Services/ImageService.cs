using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours.Framework.Infrastructure.EFCore;
using TerraMours_Gpt.Domains.GptDomain.Contracts.Req;
using TerraMours_Gpt.Domains.GptDomain.Contracts.Res;
using TerraMours_Gpt.Domains.GptDomain.Hubs;
using TerraMours_Gpt.Domains.GptDomain.IServices;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.Commons;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.GptModels;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using TerraMours_Gpt.Domains.LoginDomain.Contracts.Common;
using TerraMours.Domains.LoginDomain.IServices;
using AllInAI.Sharp.API.Dto;
using AllInAI.Sharp.API.Service;
using AllInAI.Sharp.API.Req;
using Org.BouncyCastle.Ocsp;
using AllInAI.Sharp.API.Res;
using TerraMours.Framework.Infrastructure.Contracts.SystemModels;
using k8s.KubeConfigModels;

namespace TerraMours_Gpt.Domains.GptDomain.Services
{
    public class ImageService : IImageService {
        private readonly FrameworkDbContext _dbContext;
        private readonly IOptionsSnapshot<GptOptions> _options;
        private readonly HttpClient _httpClient;
        private readonly IHubContext<GraphGenerationHub> _hubContext;
        private readonly Serilog.ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ISysUserService _sysUserService;

        public ImageService(FrameworkDbContext dbContext, IOptionsSnapshot<GptOptions> options, HttpClient httpClient, IHubContext<GraphGenerationHub> hubContext, Serilog.ILogger logger, IMapper mapper, ISysUserService sysUserService) {
            _dbContext = dbContext;
            _options = options;
            _httpClient = httpClient;
            _hubContext = hubContext;
            _logger = logger;
            _mapper = mapper;
            _sysUserService = sysUserService;
        }

        public async Task<ApiResponse<string?>> GenerateGraph(ImageReq req) {

            //添加后台任务
            BackgroundJob.Enqueue(() => BackServiceCreateImg(req));
            string message = await SendWaitingCount("img-queue");
            return ApiResponse<string?>.Success(message);
        }

        public async Task<ApiResponse<bool>> ShareImage(long ImageRecordId, bool IsPublic, long? userId) {
            var image = await _dbContext.ImageRecords.FirstOrDefaultAsync(m => m.ImageRecordId == ImageRecordId && m.Enable == true);
            if (image == null) {
                return ApiResponse<bool>.Fail("图片不存在");
            }
            image.Share(IsPublic,userId);
            _dbContext.ImageRecords.Update(image);
            await _dbContext.SaveChangesAsync();
            return ApiResponse<bool>.Success(true);
        }


        public async Task<ApiResponse<PagedRes<ImageRes>>> ShareImageList(PageReq page) {
            var imageOption = await _dbContext.GptOptions.FirstOrDefaultAsync();
            // 获取图片路径
            var baseUrl = imageOption.ImagOptions.ImagFileBaseUrl;
            var query = _dbContext.ImageRecords.Where(m => (string.IsNullOrEmpty(page.QueryString) || m.Prompt.Contains(page.QueryString)) && m.Enable == true && m.IsPublic == true);
            var total = await query.CountAsync();
            var item = await query.OrderByDescending(m => m.CreateDate).Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize).ToListAsync();
            var res = _mapper.Map<IEnumerable<ImageRes>>(item);
            //查询用户名称redis缓存
            var sysUser = await _sysUserService.GetUserNameList();
            foreach (var r in res)
            {
                r.IsPublic = null;
                r.UserName = sysUser.FirstOrDefault(m => m.Key == r.UserId).Value;

                r.ImagUrl =r.ImagUrl.StartsWith("http") ? r.ImagUrl:(baseUrl + r.ImagUrl);
            }
            return ApiResponse<PagedRes<ImageRes>>.Success(new PagedRes<ImageRes>(res, total, page.PageIndex, page.PageSize));
        }

        public async Task<ApiResponse<PagedRes<ImageRes>>> MyImageList(PageReq page, long? userId) {
            var imageOption = await _dbContext.GptOptions.FirstOrDefaultAsync();
            // 获取图片路径
            var baseUrl = imageOption.ImagOptions.ImagFileBaseUrl;
            var query = _dbContext.ImageRecords.Where(m => (string.IsNullOrEmpty(page.QueryString) || m.Prompt.Contains(page.QueryString)) && m.Enable==true && m.UserId==userId);
            var total = await query.CountAsync();
            var item = await query.OrderByDescending(m => m.CreateDate).Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize).ToListAsync();
            var res = _mapper.Map<IEnumerable<ImageRes>>(item);
            foreach (var r in res) {
                r.ImagUrl = r.ImagUrl.StartsWith("http") ? r.ImagUrl : (baseUrl + r.ImagUrl);
            }
            return ApiResponse<PagedRes<ImageRes>>.Success(new PagedRes<ImageRes>(res, total, page.PageIndex, page.PageSize));
        }

        public async Task<ApiResponse<bool>> DeleteImage(long ImageRecordId, long? userId) {
            var image = await _dbContext.ImageRecords.FirstOrDefaultAsync(m => m.ImageRecordId == ImageRecordId && m.Enable == true);
            if (image == null) {
                return ApiResponse<bool>.Fail("图片不存在");
            }
            image.Delete(userId);
            await _dbContext.SaveChangesAsync();
            return ApiResponse<bool>.Success(true);
        }
        /// <summary>
        /// 后台任务生成图片（DisableConcurrentExecution 设置超时时间 Queue设置任务类型）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [DisableConcurrentExecution(timeoutInSeconds: 180)]
        [Queue("img-queue")]
        public async Task BackServiceCreateImg(ImageReq request) {
            try {
                var user= await _dbContext.SysUsers.FirstOrDefaultAsync(m => m.UserId == request.UserId);
                if (user == null || user.Balance==null || user.Balance<(_options.Value.ImagOptions.ImagePrice * request.Count))
                {
                    _hubContext.Clients.Client(request.ConnectionId).SendAsync("updateImgUrl", $"生成图片失败：账号余额不足");
                    return;
                }
                List<string> imgList = await CreatImages(request);
                if (imgList.Count > 0)
                {
                    user.Balance -= _options.Value.ImagOptions.ImagePrice * request.Count;
                    user.ModifyDate = DateTime.Now;
                    _dbContext.SysUsers.Update(user);
                    await _dbContext.SaveChangesAsync();
                }
                _hubContext.Clients.Client(request.ConnectionId).SendAsync("updateImgUrl", imgList);
            }
            catch (Exception ex) {
                _logger.Error($"SD 生成图片报错：{ex.Message}");
                _hubContext.Clients.Client(request.ConnectionId).SendAsync("updateImgUrl", $"生成图片失败：{ex.Message}");
            }
            finally {
                SendWaitingCount("img-queue");
            }
        }
        /// <summary>
        /// 后台任务生成图片
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<string>> CreatImages(ImageReq request) {
            List<string> imgList = new List<string>();
            List<string> outImgList = new List<string>();
            var prompt=request.Prompt;
            string pranslatePrompt= string.Empty;
            imgList = await CreateImg(request);
            var imageOption = await _dbContext.GptOptions.FirstOrDefaultAsync();
            if (imgList.Count() > 0) {
                foreach (var item in imgList)
                {
                    SaveImg(prompt, pranslatePrompt, item, request.BaseType, request.Model,request.Size, request.UserId);
                    // 获取图片路径
                    var baseUrl = imageOption.ImagOptions.ImagFileBaseUrl;
                    outImgList.Add(baseUrl + item);
                }
                await _dbContext.SaveChangesAsync();
            }
            return outImgList;
        }

        public async Task<ApiResponse<PagedRes<ImageRes>>> AllImageList(PageReq page, long? userId) {
            var user= await _dbContext.SysUsers.AsNoTracking().FirstOrDefaultAsync(m => m.UserId == userId);
            var currentRole = _dbContext.SysRoles.FirstOrDefault(m => m.RoleId == user.RoleId);
            if (user == null || currentRole ==null || currentRole.IsAdmin !=true)
            {
                return ApiResponse<PagedRes<ImageRes>>.Fail("用户权限不足");
            }
            var query = _dbContext.ImageRecords.Where(m => (string.IsNullOrEmpty(page.QueryString) || m.Prompt.Contains(page.QueryString)) && m.Enable == true);
            var total = await query.CountAsync();
            var item = await query.OrderByDescending(m => m.CreateDate).Skip((page.PageIndex - 1) * page.PageSize).Take(page.PageSize).ToListAsync();
            var imageOption = await _dbContext.GptOptions.FirstOrDefaultAsync();
            var res = _mapper.Map<IEnumerable<ImageRes>>(item);
            //获取用户名称缓存
            var sysUser = await _sysUserService.GetUserNameList();
            foreach (var i in res) {
                i.UserName = sysUser.FirstOrDefault(m => m.Key == i.UserId).Value;
                // 获取图片路径
                var baseUrl = imageOption.ImagOptions.ImagFileBaseUrl;
                i.ImagUrl =i.ImagUrl.StartsWith("http") ? i.ImagUrl:(baseUrl + i.ImagUrl);
            }
            return ApiResponse<PagedRes<ImageRes>>.Success(new PagedRes<ImageRes>(res, total, page.PageIndex, page.PageSize));
        }

        #region 私有方法
        /// <summary>
        /// 查询用户图片次数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private int? ImgCount(long userId) {
            var user= _dbContext.SysUsers.FirstOrDefault(m=>m.UserId ==userId);
            return user?.ImageCount;
        }
        /// <summary>
        /// 保存记录
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="pranslatePrompt"></param>
        /// <param name="imagUrl"></param>
        /// <param name="modelType"></param>
        /// <param name="model"></param>
        /// <param name="size"></param>
        /// <param name="userId"></param>
        private void SaveImg(string? prompt, string? pranslatePrompt, string? imagUrl, int? modelType, string? model, int? size, long? userId) {
            ImageRecord record = new ImageRecord(prompt,pranslatePrompt,imagUrl,modelType,model,size,userId);
            _dbContext.ImageRecords.Add(record);
        }
        /// <summary>
        /// 文生图
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<List<string>> CreateImg(ImageReq req) {
            var size = "512x512";
            switch (req.Size) {
                case 256:
                    size = "256x256"; break;
                case 512:
                    size =req.BaseType==4? "512x512": "1024x1024"; break;
                case 1024:
                default:
                    size = "1024x1024"; break;
            }
            AuthOption authOption;
            if (req.BaseType == (int)AllInAI.Sharp.API.Enums.AITypeEnum.Baidu) {
                AuthService authService = new AuthService(req.BaseUrl);
                var token = await authService.GetTokenAsyncForBaidu(req.Key.Split(",")[0], req.Key.Split(",")[1]);
                authOption = new AuthOption() { Key = token.access_token, BaseUrl = req.BaseUrl, AIType = (AllInAI.Sharp.API.Enums.AITypeEnum)req.BaseType };
            }
            else {
                authOption = new AuthOption() { Key = req.Key, BaseUrl = req.BaseUrl, AIType = (AllInAI.Sharp.API.Enums.AITypeEnum)req.BaseType };
            }
            ImgService imgService = new ImgService(authOption);
            Txt2ImgReq imgReq = new Txt2ImgReq();
            imgReq.Steps = 20;
            imgReq.Size = size;
            imgReq.N = req.Count;
            imgReq.Prompt = req.Prompt;
            imgReq.NegativePrompt = req.NegativePrompt;
            imgReq.ResponseFormat = "b64_json";
            ImgRes imgRes = await imgService.Txt2Img(imgReq);
            if (imgRes.Error != null) {
                _logger.Error("图片报错：" + imgRes.Error.Message);
                throw new Exception("图片报错：" + imgRes.Error.Message);
            }
            else {
                var imgList = new List<string>();
                if (imgRes.Results.Count > 0) {
                    foreach (var item in imgRes.Results) {
                        var fileName = $"{Guid.NewGuid()}.png";
                        var folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
                        if (!Directory.Exists(folderPath)) {
                            Directory.CreateDirectory(folderPath);
                        }
                        var filePath = Path.Combine(folderPath, fileName);
                        // 将 Base64 数据转换为二进制数据，并写入文件
                        var imageData = Convert.FromBase64String(item.B64);
                        await System.IO.File.WriteAllBytesAsync(filePath, imageData);
                        // 生成图片 URL，注意将反斜杠转换为正斜杠
                        var imageUrl = $"/{fileName.Replace("\\", "/")}";
                        imgList.Add(imageUrl);
                    }
                }
                return imgList;
            }
        }


        /// <summary>
        /// 翻译文本openai
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        private async Task<string> TranslateOpenAi(ImageReq req)
        {
            var options = await _dbContext.GptOptions.FirstOrDefaultAsync();
            // 定义正则表达式
            Regex regex = new Regex(@"[\u4e00-\u9fa5]+");
            if (!regex.IsMatch(req.Prompt))
            {
                _logger.Information("SD prompt无需翻译。");
                return req.Prompt;
            }
            AuthOption authOption;
            if (req.BaseType == (int)AllInAI.Sharp.API.Enums.AITypeEnum.Baidu) {
                AuthService authService = new AuthService(req.BaseUrl);
                var token = await authService.GetTokenAsyncForBaidu(req.Key.Split(",")[0], req.Key.Split(",")[1]);
                authOption = new AuthOption() { Key = token.access_token, BaseUrl = req.BaseUrl, AIType = (AllInAI.Sharp.API.Enums.AITypeEnum)req.BaseType };
            }
            else {
                authOption = new AuthOption() { Key = req.Key, BaseUrl = req.BaseUrl, AIType = (AllInAI.Sharp.API.Enums.AITypeEnum)req.BaseType };
            }
            var messegs = new List<MessageDto>();
            messegs.Add(new MessageDto() { Role = "system", Content = "Translate Chinese into English" });
            messegs.Add(new MessageDto() { Role = "user", Content = req.Prompt });
            AllInAI.Sharp.API.Service.ChatService chatService = new AllInAI.Sharp.API.Service.ChatService(authOption);
            //调用SDK
            var response = await chatService.Completion(new CompletionReq {
                Messages = messegs,
                Model = options.OpenAIOptions.OpenAI.ChatModel,
                MaxTokens = 500,
            });
            return response.Choices[0].Message.Content;
        }
        /// <summary>
        /// 推送队列的等待信息
        /// </summary>
        /// <param name="enqueue">任务类型</param>
        /// <returns></returns>
        private async Task<string> SendWaitingCount(string enqueue) {
            var queueLength = JobStorage.Current.GetMonitoringApi()
                            .EnqueuedCount(enqueue);
            string message = $"任务已提交，您前面还有 {queueLength} 个任务正在等待。";
            await _hubContext.Clients.All.SendAsync("updateWaitingCount", queueLength);
            return message;
        }

        #endregion
    }
}
