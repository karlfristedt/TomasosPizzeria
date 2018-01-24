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

        }
        public static async Task CreateAdminUser(IServiceProvider serviceProvider, IConfiguration configuration)
        {

            UserManager<ApplicationUser> userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync(configuration["Data:AdminUser:UserName"]) == null)
            {
                var adminuser = new ApplicationUser
                {
                    UserName = configuration["Data:AdminUser:UserName"]      
                };

                var result = await userManager.CreateAsync(adminuser, configuration["Data:AdminUser:Password"]);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminuser, configuration["Data:AdminUser:Role"]);
                }


            }
            

        }
        public static async Task CreateRegularUser(IServiceProvider serviceProvider, IConfiguration configuration)
        {

            UserManager<ApplicationUser> userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync(configuration["Data:RegularUser:UserName"]) == null)
            {
                var regularuser = new ApplicationUser
                {
                    UserName = configuration["Data:RegularUser:UserName"]
                };

                var result = await userManager.CreateAsync(regularuser, configuration["Data:RegularUser:Password"]);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(regularuser, configuration["Data:RegularUser:Role"]);
                }


            }

        }
        public static async Task CreatePremiumUser(IServiceProvider serviceProvider, IConfiguration configuration)
        {

            UserManager<ApplicationUser> userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync(configuration["Data:PremiumUser:UserName"]) == null)
            {
                var premiumuser = new ApplicationUser
                {
                    UserName = configuration["Data:PremiumUser:UserName"]
                };

                var result = await userManager.CreateAsync(premiumuser, configuration["Data:PremiumUser:Password"]);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(premiumuser, configuration["Data:PremiumUser:Role"]);
                }


            }

        }

    }

}
