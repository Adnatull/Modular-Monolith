using Microsoft.AspNetCore.Identity;
using Module.Identity.Core.Entities;
using Module.Identity.Core.Extensions;
using Module.Identity.Core.Repositories;
using Module.Shared.Response;

namespace Module.Identity.Infrastructure.Repositories {
    public class ApplicationUserManager : IApplicationUserManager {

        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserManager(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;

        }
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password) {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<ApplicationUser> GetUserByNameAsync(string userName) {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<Response<int>> RegisterUserAsync(ApplicationUser user) {
            if (user.Email != null && await _userManager.FindByEmailAsync(user.Email) != null) {
                return Response<int>.Fail("Email already exists! Please try a different one!");
            }
            user.UserName ??= user.Email;
            if (user.UserName != null && await _userManager.FindByNameAsync(user.UserName) != null) {
                return Response<int>.Fail("User Name already exists! Please try a different one");
            }
            var rs = await _userManager.CreateAsync(user);
            return rs.ToIdentityResponse();
        }
    }
}
