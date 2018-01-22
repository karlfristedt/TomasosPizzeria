using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TomasosPizzeria.Models
{
    public class TomasosIdentityDBContext : IdentityDbContext<ApplicationUser>
    {
        public TomasosIdentityDBContext(DbContextOptions<TomasosIdentityDBContext> options) : base(options) { }

        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (await roleManager.FindByNameAsync(configuration["Data:Roles:Admin"]) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(configuration["Data:Roles:Admin"]));
            }

            if (await roleManager.FindByNameAsync(configuration["Data:Roles:RegularUser"]) == null)
            {
                    await roleManager.CreateAsync(new IdentityRole(configuration["Data:Roles:RegularUser"]));
            }

            if (await roleManager.FindByNameAsync(configuration["Data:Roles:PremiumUser"]) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(configuration["Data:Roles:PremiumUser"]));
            }

            //AppUser user = new AppUser { UserName = username, Email = email };
            //IdentityResult result = await userManager.CreateAsync(user, password); if (result.Succeeded) { await userManager.AddToRoleAsync(user, role); }



        }
        public static async Task CreateAdminUser(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            UserManager<ApplicationUser> userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync(configuration["Data:AdminUser:UserName"]) == null)
            {
                var adminuser = new ApplicationUser
                {
                    UserName = configuration["Data:AdminUser:UserName"],
                    Email = configuration["Data:AdminUser:Email"],
                    Name = configuration["Data:AdminUser:Name"],
                };

                var result = await userManager.CreateAsync(adminuser, configuration["Data:AdminUser:Password"]);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminuser, configuration["Data:AdminUser:Role"]);
                }


            }

            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (await roleManager.FindByNameAsync("RegularUser") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("RegularUser"));
            }
            if (await roleManager.FindByNameAsync("PremiumUser") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("PremiumUser"));
            }

            //AppUser user = new AppUser { UserName = username, Email = email };
            //IdentityResult result = await userManager.CreateAsync(user, password); if (result.Succeeded) { await userManager.AddToRoleAsync(user, role); }



        }

    }

}
