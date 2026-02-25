using Microsoft.AspNetCore.Identity;
using DevSpot.Constants;
namespace DevSpot.Data
{
    public class UserSeeder
    {
        public static async Task SeedUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            await SeedUserswithRoles(userManager, "Admin@google.com", "Admin123!!@", Roles.Admin);
            await SeedUserswithRoles(userManager, "jobseeker@google.com", "Jobseeker123!!@", Roles.JobSeeker);
            await SeedUserswithRoles(userManager, "employer@google.com", "Employer123!!@", Roles.Employer);
        }
        public static async Task SeedUserswithRoles(UserManager<IdentityUser> userManager,string email, string password, string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email
                };
                var Result = await userManager.CreateAsync(user, password);
                if (Result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    throw new Exception($"errors: {string.Join(" | ", Result.Errors.Select(e => e.Description))}");
                }
            }
            }
    }
}
