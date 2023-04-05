using Microsoft.Extensions.Hosting;

namespace Module.Identity.WebApi.MigrationManager {
    public static class IdentityMigrationManager {
        public static IHost IdentityMigrateAndSeed(this IHost host) {
            Infrastructure.Seeds.IdentityMigrationManager.MigrateDatabaseAsync(host).GetAwaiter().GetResult();
            Infrastructure.Seeds.IdentityMigrationManager.SeedDatabaseAsync(host).GetAwaiter().GetResult();
            return host;
        }
    }
}
