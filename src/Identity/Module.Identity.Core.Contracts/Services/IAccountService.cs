using Module.Identity.Core.Contracts.DataTransferObjects;
using Module.Shared.Response;

namespace Module.Identity.Core.Contracts.Services {
    public interface IAccountService {
        Task<Response<UserIdentityDto>> CheckPasswordAsync(LoginUserDto loginUserDto);
    }
}
