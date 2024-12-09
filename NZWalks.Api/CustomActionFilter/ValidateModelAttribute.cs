using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NZWalks.Api.CustomActionFilter;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.ModelState.IsValid is false)
        {
            context.Result = new BadRequestResult();
        }
    }
}