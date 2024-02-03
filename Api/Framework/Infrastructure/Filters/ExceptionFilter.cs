using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TerraMours.Framework.Infrastructure.Filters
{
    //无效，改用中间件替换方案IEndpointFilter
    public class ExceptionFilter : ExceptionFilterAttribute
    //public class ExceptionFilter : IEndpointFilter
    {
        public ExceptionFilter()
        {

        }
        private readonly Serilog.ILogger _logger;
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="logger"></param>
        public ExceptionFilter(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 全局捕获异常方法
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                context.Result = new JsonResult(new { Code = 500, Message = context.Exception.Message, Data = "接口发生错误" });
                string ActionRoute = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).DisplayName;
                _logger.Error("请求路径：{0}，错误信息：{1}", ActionRoute, context.Exception.Message);
                context.ExceptionHandled = true;
            }
        }
    }
}
