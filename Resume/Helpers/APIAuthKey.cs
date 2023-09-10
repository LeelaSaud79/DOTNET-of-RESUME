using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Resume.Helpers
{

    public class APIAuthKey : Attribute, IAsyncActionFilter
    {
        private const string ApiUsername = "user";
        private const string ApiPassword = "pass";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiUsername, out var username) || !context.HttpContext.Request.Headers.TryGetValue(ApiPassword, out var passkey))
            {
                context.Result = new BadRequestResult();
                return;
            }
            else
            {
                bool check = PasswordVerify(username, passkey);
                if (!check)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
            await next();
        }
        public static bool PasswordVerify(String username, String passkey)
        {
            if (username == "lila" && passkey == "12345")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}




