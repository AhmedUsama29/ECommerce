using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class ReddisCacheAttribute(int durationInSec = 600) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var key = CreateCacheKey(context.HttpContext.Request);

            var cacheValue = await CacheService.GetAsync(key);

            if (cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }
            else
            {
                var executedContext = await next.Invoke();

                if (executedContext.Result is OkObjectResult result)
                    await CacheService.SetAsync(key, result.Value, TimeSpan.FromSeconds(durationInSec)!);
            }
        }

        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(request.Path + "?");

            foreach (var item in request.Query.OrderBy(q => q.Key))
            {
                builder.Append($"{item.Key}={item.Value}&");
            }

            return builder.ToString().TrimEnd('&');
        }
    }
}
