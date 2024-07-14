using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Tech.Challenge4.API.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {

                await HandlerExceptionAsync(context, ex);
            }
        }

        private static Task HandlerExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            switch (ex)
            {
                case ValidationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
            }

            context.Response.StatusCode = statusCode;

            context.Response.ContentType = "application/json";

            var response = new
            {
                error = ex.Message
            };

            var jsonResponse = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
