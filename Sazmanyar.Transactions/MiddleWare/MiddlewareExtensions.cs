using Microsoft.AspNetCore.Diagnostics;

namespace Sazmanyar.Transactions.MiddleWare
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
