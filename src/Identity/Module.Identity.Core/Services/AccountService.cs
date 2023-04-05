using AutoMapper;
using Module.Identity.Core.DataTransferObjects;
using Module.Identity.Core.Entities;
using Module.Identity.Core.IServices;
using Module.Identity.Core.Repositories;
using Module.Shared.Response;

namespace Module.Identity.Core.Services {
    public class AccountService : IAccountService {

        private readonly IApplicationUserManager _userManager;
        private readonly IMapper _mapper;
        public AccountService(IApplicationUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<Response<UserIdentityDto>> CheckPasswordAsync(LoginUserDto loginUserDto) {
            var user = await _userManager.GetUserByNameAsync(loginUserDto.UserName);
            if (user == null) return Response<UserIdentityDto>.Fail("UserName does not exists");

            var rs = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
            return rs
                ? Response<UserIdentityDto>.Success(new UserIdentityDto { Id = user.Id }, "Login Success")
                : Response<UserIdentityDto>.Fail("Password/Username is not correct");
        }

        public async Task<Response<UserIdentityDto>> RegisterUserAsync(RegisterUserDto registerUserDto) {

            var user = _mapper.Map<ApplicationUser>(registerUserDto);
            var rs = await _userManager.RegisterUserAsync(user);
            return rs.Succeeded
                ? Response<UserIdentityDto>.Success(new UserIdentityDto { Id = user.Id }, rs.ToString())
                : Response<UserIdentityDto>.Fail(rs.ToString()); 
        }
    }
}
