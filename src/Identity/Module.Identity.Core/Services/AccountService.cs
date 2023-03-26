using Module.Identity.Core.DataTransferObjects;
using Module.Identity.Core.IServices;
using Module.Shared.Response;

namespace Module.Identity.Core.Services {
    public class AccountService : IAccountService {
        public Task<Response<UserIdentityDto>> CheckPasswordAsync(LoginUserDto loginUserDto) {
            throw new NotImplementedException();
        }
    }
}
