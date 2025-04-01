using LoginRegister.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoginRegister.Data
{
    public class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

            // Ensure database is created
           // await context.Database.MigrateAsync();

            // Ensure roles
            var roles = new[] { "Admin", "JobSeeker", "Employer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }

            // Seed default admin if not present
            var adminEmail = "admin@app.com";
            var adminPassword = "Admin@123";
            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin == null)
            {
                var adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Super",
                    LastName = "Admin",
                    Role = "Admin",
                    PhoneNumber = "01700000000",
                    ProfilePicture = string.Empty,
                    Gender = "System",
                    DateOfBirth = DateTime.MinValue
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");

                    context.Admins.Add(new Admin
                    {
                        UserID = adminUser.Id
                    });

                    await context.SaveChangesAsync();
                }

            }
        }
    }
}
