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

                if(context.Response.StatusCode == (int)HttpStatusCode.NotFound)
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
            catch (Exception ex)
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
                    _ => (int)HttpStatusCode.InternalServerError
                };

                context.Response.StatusCode = response.StatusCode;

                var JsonRes = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(JsonRes);

                

            }

        }

    }
}
