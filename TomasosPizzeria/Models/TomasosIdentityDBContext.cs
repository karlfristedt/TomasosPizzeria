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
                    UserName = configuration["Data:AdminUser:UserName"],
                    Email = configuration["Data:AdminUser:Email"],
                    Name = configuration["Data:AdminUser:Name"],
                    Adress = configuration["Data:AdminUser:Adress"],
                    PhoneNumber = configuration["Data:AdminUser:Phone"],
                    City = configuration["Data:AdminUser:City"],
                    PostalCode = configuration["Data:AdminUser:PostalCode"]
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
                    UserName = configuration["Data:RegularUser:UserName"],
                    Email = configuration["Data:RegularUser:Email"],
                    Name = configuration["Data:RegularUser:Name"],
                    Adress = configuration["Data:RegularUser:Adress"],
                    PhoneNumber = configuration["Data:RegularUser:Phone"],
                    City = configuration["Data:RegularUser:City"],
                    PostalCode = configuration["Data:RegularUser:PostalCode"]
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
                    UserName = configuration["Data:PremiumUser:UserName"],
                    Email = configuration["Data:PremiumUser:Email"],
                    Name = configuration["Data:PremiumUser:Name"],
                    PhoneNumber = configuration["Data:PremiumUser:Phone"],
                    City = configuration["Data:PremiumUser:City"],
                    PostalCode = configuration["Data:PremiumUser:PostalCode"],
                    Adress = configuration["Data:PremiumUser:Adress"]
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
