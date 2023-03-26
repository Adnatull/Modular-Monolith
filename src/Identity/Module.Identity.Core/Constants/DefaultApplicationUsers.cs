using Module.Identity.Core.Entities;

namespace Module.Identity.Core.Constants {
    public class DefaultApplicationUsers {
        public static ApplicationUser GetSuperUser() {
            var defaultUser = new ApplicationUser {
                UserName = "SuperAdmin",
                Email = "a2masum@yahoo.com",
                FirstName = "Al",
                LastName = "Masum",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            return defaultUser;
        }
    }
}
