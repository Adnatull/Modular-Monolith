using System.Collections.Generic;
using System.Security.Claims;

namespace Module.Shared.Permissions {
    public interface IPermissionHelper {
        List<Claim> GetAllPermissions();
    }
}
