using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Module.Shared.Response;
using System;
using System.Linq;

namespace Module.Shared.Filters {
    public class GlobalValidationFilter : IActionFilter {
        public void OnActionExecuted(ActionExecutedContext context) {
            
        }

        public void OnActionExecuting(ActionExecutingContext context) {
            if(!context.ModelState.IsValid) {
                var errors = from state in context.ModelState.Values
                             from error in state.Errors
                             select error.ErrorMessage;

                var rs =  Response<string>.Fail("One or more validation errors occurred.", errors.ToList());
                context.Result = new BadRequestObjectResult(rs);
            }
        }
    }
}
