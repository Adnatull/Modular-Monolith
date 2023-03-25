using Microsoft.AspNetCore.Identity;
using Module.Identity.Core.Entities;
using Module.Identity.Core.Repositories;

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
    }
}
