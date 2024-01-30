namespace TerraMours.Framework.Infrastructure.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;  // 用来处理上下文请求
        private readonly Serilog.ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, Serilog.ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 执行中间件
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
    /*    public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); //要么在中间件中处理，要么被传递到下一个中间件中去
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex); // 捕获异常了 在HandleExceptionAsync中处理
            }
        }*/

        /// <summary>
        /// 异步处理异常
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        /* private async Task HandleExceptionAsync(HttpContext context, Exception exception)
         {
             context.Response.ContentType = "application/json";  // 返回json 类型
             var response = context.Response;
             //这里是mvc那一套的ActionTResult  ActionTResult要改成我们设计的返回的统一类型
             *//*         var errorResponse = new ActionTResult
                      {
                          Succes = SuccessTypeEnum.Error
                      };  // 自定义的异常错误信息类型
             */

        /*    switch (exception)
            {
                case ApplicationException ex:
                    if (ex.Message.Contains("Invalid token"))
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.ErrorMsg = ex.Message;
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.ErrorMsg = ex.Message;
                    break;
                case KeyNotFoundException ex:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.ErrorMsg = ex.Message;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.ErrorMsg = "Internal Server errors. Check Logs!";
                    break;
            }
            _logger.Error(exception.Message);
            var result = JsonSerializer.Serialize(errorResponse);*//*
        await context.Response.WriteAsync(result);
    }*/
    }
}
