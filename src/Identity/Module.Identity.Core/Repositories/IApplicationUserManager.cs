using Module.Identity.Core.Entities;

namespace Module.Identity.Core.Repositories {
    public interface IApplicationUserManager {
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<ApplicationUser> GetUserByNameAsync(string userName);
    }
}
