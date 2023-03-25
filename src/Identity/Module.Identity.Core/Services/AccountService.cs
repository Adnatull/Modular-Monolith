using Module.Identity.Core.Contracts.DataTransferObjects;
using Module.Identity.Core.Contracts.Services;
using Module.Shared.Response;

namespace Module.Identity.Core.Services {
    public class AccountService : IAccountService {
        public Task<Response<UserIdentityDto>> CheckPasswordAsync(LoginUserDto loginUserDto) {
            throw new NotImplementedException();
        }
    }
}
