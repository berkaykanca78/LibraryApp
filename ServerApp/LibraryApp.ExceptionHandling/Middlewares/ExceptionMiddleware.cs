using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LibraryApp.ExceptionHandling.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;

        public ExceptionHandlerMiddleware(RequestDelegate Next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            next = Next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            logger.LogInformation("Console Üzerinden Log Yazmaya Başladık.");
            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                // Hata Yönetimi
                logger.LogError(ex.Message);

                logger.LogInformation("Console Üzerinden Log Yazmayı Bitirdik.");
            }
        }
    }
}
