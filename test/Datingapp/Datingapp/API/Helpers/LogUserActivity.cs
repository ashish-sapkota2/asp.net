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
            var uow = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
            var user = await uow.UserRepository.GetByUsername(username);
            user.LastActive = DateTime.Now;
            await uow.Complete();
        }
    }
}
