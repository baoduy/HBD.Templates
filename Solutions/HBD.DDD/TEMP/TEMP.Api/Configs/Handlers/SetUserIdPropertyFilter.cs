using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using TEMP.AppServices.Share;
using TEMP.Core;
using TEMP.Core.Options;

namespace TEMP.Api.Configs.Handlers;

public class SetUserIdPropertyFilter : IActionFilter
{
    private readonly FeatureOptions _options;

    public SetUserIdPropertyFilter(IOptions<FeatureOptions> options) => _options = options.Value;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        string userName;

        if (_options.RequireAuthorization)
        {
            if (context.HttpContext.User.Identity?.IsAuthenticated != true)
                return;
            userName = context.HttpContext.User.Identity.Name!;
        }
        else userName = SysConsts.SystemAccount;

        foreach (var a in context.ActionArguments)
        {
            if (a.Value is not ModelBase b) continue;
            
            b.UserId = userName;
            Console.WriteLine("Set user for model: " + b.GetType().Name);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
       
    }
}