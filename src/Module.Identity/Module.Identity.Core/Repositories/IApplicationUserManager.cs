using Module.Identity.Core.Entities;
using Module.Shared.Response;

namespace Module.Identity.Core.Repositories {
    public interface IApplicationUserManager {
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<ApplicationUser> GetUserByNameAsync(string userName);
        Task<Response<int>> RegisterUserAsync(ApplicationUser user);
    }
}
