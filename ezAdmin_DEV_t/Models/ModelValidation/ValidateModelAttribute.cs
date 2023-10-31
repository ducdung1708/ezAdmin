using Models.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Models.ModelValidation
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }

        public class ValidationFailedResult : ObjectResult
        {
            public ValidationFailedResult(ModelStateDictionary modelState)
                : base(new BaseResponse<object>(modelState))
            {
            }
        }
    }
}
