using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    internal class DbInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {


            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));

            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));

            }

            if (await roleManager.FindByNameAsync("BossCar") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("BossCar"));

            }
            if (await roleManager.FindByNameAsync("BossDriver") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("BossDriver"));

            }
            if (await roleManager.FindByNameAsync("Engineer") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Engineer"));

            }
           
            string adminNik = "Boss";
            string password = "Admin1$$$";
            if (await userManager.FindByNameAsync(adminNik) == null)
            {
                User admin = new User
                {
                    UserName = adminNik,
                    PositionOffice = "admin-admin",
                    FullName="Иванов"
                };
               
                IdentityResult result = userManager.CreateAsync(admin, password).Result;
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");

                }
              

            }
        }
    }
}
