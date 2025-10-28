using AirportProject4.Project.core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace AirportProject4.Shared.DbInitializerService
{
    public class DbInitializer: IDbInitializer
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private List<string> roles = new List<string>() { "Admin", "SuperAdmin", "User" };
        public DbInitializer(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task InitializeIdentityAsync()
        {
            foreach (var role in roles)
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new AppRole { Name = role });

            if (!userManager.Users.Any())
            {
                var admin = new AppUser
                {
                    UserName = "admin",
                    Email = "admin@super.com",
                    FullName = "Admin User",
                    PassportNumber = "ADMIN12345",

                };

                var superadmin = new AppUser
                {

                    UserName = "SA12",
                    Email = "super@super.com",
                    FullName = "Super Admin User",
                    PassportNumber = "SUPER12345",


                };


                await userManager.CreateAsync(admin, "P@ssw0rd");
                await userManager.CreateAsync(superadmin, "SuperP@ssw0rd");

                await userManager.AddToRoleAsync(admin, "Admin");
                await userManager.AddToRoleAsync(superadmin, "SuperAdmin");
            }  
        }

    }
}
