using Microsoft.AspNetCore.Identity;

namespace Module.Identity.Core.Entities {
    public class ApplicationRole : IdentityRole {
        public ApplicationRole(string name) : base(name) {

        }
        public string Description { get; set; }
    }
}
