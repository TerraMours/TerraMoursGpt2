using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using TerraMours.Domains.LoginDomain.Contracts.Common;
using TerraMours.Domains.LoginDomain.Contracts.Req;
using TerraMours.Domains.LoginDomain.IServices;
using TerraMours.Framework.Infrastructure.Contracts.Commons;
using TerraMours.Framework.Infrastructure.EFCore;
using TerraMours.Framework.Infrastructure.Utils;

namespace TerraMours.Domains.LoginDomain.Services
{
    public class EmailService : IEmailService
    {

        private readonly IOptionsSnapshot<SysSettings> _sysSettings;
        private readonly FrameworkDbContext _dbContext;
        public EmailService(FrameworkDbContext dbContext, IOptionsSnapshot<SysSettings> sysSettings)
        {
            _sysSettings = sysSettings;
            _dbContext= dbContext;
        }

        /// <summary>
        /// 生成用户注册六位数验证码，存在cache用5分钟过期
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiResponse<bool>> CreateCheckCode(EmailReq req)
        {
            try
            {
                //生成随机六位数验证码
                var checkCode = GenerateRandomSixDigitNumber();
                //1.得先发送邮件 引用MailKit类库
                await SendEmailAsync(req.UserEmail, checkCode);
                //3.以邮箱账号为key value存验证码，五分钟过期。
                CacheHelper.SetCache(req.UserEmail, checkCode, 300);
                return  ApiResponse<bool>.Success(true);
            }
            catch (Exception ex)
            {

                throw new Exception("" + ex.Message);
            }

        }

        /// <summary>
        /// 生成随机六位数验证码
        /// </summary>
        /// <returns></returns>
        public string GenerateRandomSixDigitNumber()
        {
            try
            {
                Random random = new Random();
                int number = random.Next(100000, 999999);
                return number.ToString();
            }
            catch (Exception ex)
            {

                throw new Exception("" + ex.Message);
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="recipientEmail">收件人邮箱</param>
        /// <param name="subject">主题</param>
        /// <param name="message">邮件内容就是验证码</param>
        /// <returns></returns>
        public async Task SendEmailAsync(string recipientEmail, string message)
        {
            try
            {
                Email emailSetting = _dbContext.SysSettings.FirstOrDefault().Email != null ? _dbContext.SysSettings.FirstOrDefault().Email : _sysSettings.Value.email;
                string subject = "TerraMours系统验证码";

                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(emailSetting.SenderName, emailSetting.SenderEmail));
                emailMessage.To.Add(new MailboxAddress("", recipientEmail));
                emailMessage.Subject = subject;

                emailMessage.Body = new TextPart("plain")
                {
                    Text = "【验证码：】" + message
                };

                using var client = new SmtpClient();

                //SecureSocketOptions.StartTls
                await client.ConnectAsync(emailSetting.Host, emailSetting.Port, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(emailSetting.SenderEmail, emailSetting.SenderPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {

                throw new Exception("" + ex.Message);
            }

        }

    }
}
