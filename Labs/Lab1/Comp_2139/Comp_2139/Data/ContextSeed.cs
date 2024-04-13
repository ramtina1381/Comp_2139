using System;
using Comp_2139.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace Comp_2139.Data
{
	public class ContextSeed
	{
		public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			await roleManager.CreateAsync(new IdentityRole(Enum.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Basic.ToString()));
        }

        public static async Task SuperSeedRoleAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var superUser = new ApplicationUser
            {
                UserName = "superAdmin",
                Email = "adminsupport@domain.com",
                FirstName = "Super",
                LastName = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if(userManager.Users.All(u => u.Id != superUser.Id))
            {
                var users = await userManager.FindByEmailAsync(superUser.Email);
                if(users == null)
                {
                    await userManager.CreateAsync(superUser, "P@ssword12$");

                    await userManager.AddToRoleAsync(superUser, Enum.Roles.SuperAdmin.ToString());
                    await userManager.AddToRoleAsync(superUser, Enum.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(superUser, Enum.Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(superUser, Enum.Roles.Basic.ToString());

                }
            }
        }
    }
}

