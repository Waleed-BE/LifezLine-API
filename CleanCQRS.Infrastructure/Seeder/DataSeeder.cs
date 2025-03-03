using CleanCQRS.Core.Entities;
using CleanCQRS.Core.Enums;
using CleanCQRS.Infrastructure.ApplicationDbContext;

namespace CleanCQRS.Infrastructure.Seeder
{
    public class DataSeeder
    {
        public static void SeedRoles(AppDbContext dbContext)
        {
            if (!dbContext.TblRole.Any()) // Check if roles exist
            {
                var roles = Enum.GetValues(typeof(RoleEnum))
                                .Cast<RoleEnum>()
                                .Select(e => new Role { RoleName = e.ToString(), IsActive = true })
                                .ToList();

                dbContext.TblRole.AddRange(roles);
                dbContext.SaveChanges();
            }
        }
    }
}
