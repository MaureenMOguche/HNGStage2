using HNGStage2.Models;
using Microsoft.Extensions.Hosting;

namespace HNGStage2.Middlewares
{
    public class GlobalErrorHandling : IMiddleware
    {
        private readonly ILogger<GlobalErrorHandling> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GlobalErrorHandling(ILogger<GlobalErrorHandling> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "{Message}", ex.Message);

                ApiResponse response = new()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                };

                if (_hostEnvironment.IsDevelopment())
                {
                    response.Message = ex.Message;
                    if (ex.StackTrace != null)
                        response.Message += ex.StackTrace;
                }

                else
                    response.Message = "Operation failed";

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
