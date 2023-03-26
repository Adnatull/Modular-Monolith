using Module.Identity.Core.Constants;
using Module.Shared.Permissions;
using System.Reflection;
using System.Security.Claims;

namespace Module.Identity.Core.Permissions {
    public class PermissionHelper : IPermissionHelper
    {
        public List<Claim> GetAllPermissions()
        {
            var allPermissions = new List<Claim>();
            var permissionClass = typeof(Module.Shared.Permissions.Permissions);
            var allModulesPermissions = permissionClass.GetNestedTypes().Where(x => x.IsClass).ToList();
            foreach (var modulePermissions in allModulesPermissions)
            {
                var permissions = modulePermissions.GetFields(BindingFlags.Static | BindingFlags.Public);
                allPermissions.AddRange(permissions.Select(permission =>
                    new Claim(CustomClaimTypes.Permission, permission.GetValue(null).ToString())));
            }
            return allPermissions;
        }
    }
}
