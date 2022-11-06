    using BlogApp.Api.Commons.Exceptions;
using Newtonsoft.Json;
using Serilog;

namespace BlogApp.Api.Commons.Middlewares
{
    public class ExceptionHandlerMiddlewar
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddlewar(RequestDelegate request)
        {
            _next = request;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (StatusCodeException exception)
            {
                await HandleAsync(exception, httpContext);
            }
            catch (Exception exception)
            {
                await HandleOtherExceptionAsync(exception, httpContext);
            }
        }

        public async Task HandleOtherExceptionAsync(Exception exception, HttpContext httpContext)
        {
            Log.Error(exception, "WebAPI Global Exception Handler");
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "application/json";
            string json = JsonConvert.SerializeObject(new { Message = exception.Message });
            await httpContext.Response.WriteAsync(json);
        }

        public async Task HandleAsync(StatusCodeException exception, HttpContext httpContext)
        {
            Log.Error(exception, "WebAPI Global Exception Handler");
            httpContext.Response.StatusCode = (int)exception.HttpStatusCode;
            httpContext.Response.ContentType = "application/json";
            string json = JsonConvert.SerializeObject(new { Message = exception.Message });
            await httpContext.Response.WriteAsync(json);
        }
    }
}
