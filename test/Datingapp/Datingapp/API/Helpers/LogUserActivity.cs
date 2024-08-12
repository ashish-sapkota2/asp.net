using Datingapp.API.Extensions;
using Datingapp.API.Interface;
using Microsoft.AspNetCore.Mvc.Filters;
using Datingapp.API.Data;

namespace Datingapp.API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var username = resultContext.HttpContext.User.GetUsername();
            var repo = resultContext.HttpContext.RequestServices.GetService<UserRepository>();
            var user = await repo.GetByUsername(username);
            user.LastActive = DateTime.Now;
            await repo.SaveAllAsync();
        }
    }
}
