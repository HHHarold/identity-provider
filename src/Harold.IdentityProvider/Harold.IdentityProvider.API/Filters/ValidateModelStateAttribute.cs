using Harold.IdentityProvider.API.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Harold.IdentityProvider.API.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
            base.OnActionExecuting(context);
        }
    }
}
