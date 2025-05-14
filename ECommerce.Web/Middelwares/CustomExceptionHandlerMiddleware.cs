using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace ECommerce.Web.Middelwares
{
    public class CustomExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context) 
        {

            try
            {
                await _next.Invoke(context);

                await HandleNotFoundEndPoint(context);
            }
            catch (Exception ex)
            {
                await HandleCatchException(context, ex);

            }

        }

        private async Task HandleCatchException(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "Something Went Wrong");

            //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails()
            {
                //StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            };


            response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                UnAuthorizedException => (int)HttpStatusCode.Unauthorized,
                BadRequestException badRequestException=> GetValidationErrors(badRequestException, response),
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = response.StatusCode;

            var JsonRes = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(JsonRes);
        }

        private int GetValidationErrors(BadRequestException badRequestException, ErrorDetails response)
        {
            response.Errors = badRequestException.Errors;
            return (int)HttpStatusCode.BadRequest;
        }

        private static async Task HandleNotFoundEndPoint(HttpContext context)
        {
            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                context.Response.ContentType = "application/json";
                var response = new ErrorDetails()
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ErrorMessage = $"End Point With This Path : {context.Request.Path} Is Not Found"
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
