using Microsoft.AspNetCore.Builder;

namespace TheTeacher.Api.Framework
{
    public static class Extensions
    {
        public static void UseMyExceptionMiddleware(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(MyExceptionHandlerMiddleware));
    }
}