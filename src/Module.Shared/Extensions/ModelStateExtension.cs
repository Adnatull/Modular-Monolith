using Microsoft.AspNetCore.Mvc.ModelBinding;
using Module.Shared.Response;
using System.Linq;


namespace Module.Shared.Extensions {
    public static class ModelStateExtension {
        public static Response<string> ToValidationErrorResponse(this ModelStateDictionary modelState) {


            var errors = from state in modelState.Values
                         from error in state.Errors
                         select error.ErrorMessage;         

            return Response<string>.Fail("One or more validation errors occurred.", errors.ToList());
        }
    }
}
