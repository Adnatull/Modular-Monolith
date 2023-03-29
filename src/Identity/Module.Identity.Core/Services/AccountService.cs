using Module.Identity.Core.DataTransferObjects;
using Module.Identity.Core.IServices;
using Module.Identity.Core.Repositories;
using Module.Shared.Response;

namespace Module.Identity.Core.Services {
    public class AccountService : IAccountService {

        private readonly IApplicationUserManager _userManager;
        public AccountService(IApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
        public async Task<Response<UserIdentityDto>> CheckPasswordAsync(LoginUserDto loginUserDto) {
            var user = await _userManager.GetUserByNameAsync(loginUserDto.UserName);
            if (user == null) return Response<UserIdentityDto>.Fail("UserName does not exists");

            var rs = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
            return rs
                ? Response<UserIdentityDto>.Success(new UserIdentityDto { Id = user.Id }, "Login Success")
                : Response<UserIdentityDto>.Fail("Password/Username is not correct");
        }
    }
}
