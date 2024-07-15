using System.Net;
using System.Text.Json;
using Test.Errors;

namespace Test.API.Middleware
{
    public class ExceptionMiddeWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddeWare> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddeWare(RequestDelegate next, ILogger<ExceptionMiddeWare>logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode =(int)HttpStatusCode.InternalServerError;

                var response = env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiException(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
