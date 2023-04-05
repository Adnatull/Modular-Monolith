using Module.Identity.Core.DataTransferObjects;
using Module.Shared.Response;

namespace Module.Identity.Core.IServices {
    public interface IAccountService {
        Task<Response<UserIdentityDto>> CheckPasswordAsync(LoginUserDto loginUserDto);
        Task<Response<UserIdentityDto>> RegisterUserAsync(RegisterUserDto registerUserDto);
    }
}
