using Microsoft.AspNetCore.Diagnostics;
using System.Runtime.InteropServices.Marshalling;

namespace Products.backend.ExceptionHdl
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            this.logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(
                                                HttpContext httpContext, 
                                                Exception exception, 
                                                CancellationToken cancellationToken)
        {
            logger.LogError("Error {@exception} path {@path} Method {@method}",exception,httpContext.Request.PathBase,httpContext.Request.Method);


            var error = MapErrors.MapException(exception);

            await Results.Problem(title:error.Message,statusCode:error.status)
                .ExecuteAsync(httpContext);

            return true;
        }
    }
}
