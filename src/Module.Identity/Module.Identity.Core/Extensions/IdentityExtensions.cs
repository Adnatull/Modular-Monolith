using Microsoft.AspNetCore.Identity;
using Module.Shared.Response;

namespace Module.Identity.Core.Extensions {
    public static class IdentityExtensions {
        public static Response<int> ToIdentityResponse(this IdentityResult identityResult) {
            
            return identityResult.Succeeded
                ? Response<int>.Success(identityResult.ToString())
                : Response<int>.Fail(identityResult.ToString(), identityResult.Errors.Select(x => x.Description).ToList());
        }
    }
}
