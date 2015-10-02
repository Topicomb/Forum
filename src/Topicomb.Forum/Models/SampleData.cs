using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;

namespace Topicomb.Forum.Models
{
	public static class SampleData
	{
		public async static Task InitDB(IServiceProvider services)
		{
			var DB = services.GetRequiredService<ForumContext> ();
			var UserManager = services.GetRequiredService<UserManager<User>> ();
			var RoleManager = services.GetRequiredService<RoleManager<IdentityRole<long>>> ();
            var LocalizationManager = services.GetRequiredService<CodeCombLocalization>();
            var Configuration = services.GetRequiredService<IConfiguration>();

            if (DB.Database != null && await DB.Database.EnsureCreatedAsync())
			{
				// Creating Roles
				await RoleManager.CreateAsync(new IdentityRole<long> { Name = "Root" });
                await RoleManager.CreateAsync(new IdentityRole<long> { Name = "Super Moderator" });
				await RoleManager.CreateAsync(new IdentityRole<long> { Name = "Moderator" });
				await RoleManager.CreateAsync(new IdentityRole<long> { Name = "Member" });
				await RoleManager.CreateAsync(new IdentityRole<long> { Name = "Blocked" });
				
				// Creating Root User
				var user = new User { UserName = Configuration["Installation:RootUserName"], Email = "admin@codecomb.com" };
				var result2 = await UserManager.CreateAsync(user, Configuration["Installation:Password"]);
                await UserManager.SetTwoFactorEnabledAsync(user, false);
                foreach (var e in result2.Errors)
                    throw new Exception(e.Description);
				await UserManager.AddToRoleAsync(user, "Root");

                // Creating Sample Data
                var defaultForum = new Forum
                {
                    Url = "default",
                    ExternalUrl = null,
                    Description = "Default forum",
                    PRI = 0,
                    IsNoTopic = true,
                    ParentId = null,
                    IconId = null,
                    Password = null,
                    Title = LocalizationManager.c("Default Template"),
                    Performance = ForumPerformance.Horizontal,
                    Highlight = null
				};
                DB.Forums.Add(defaultForum);

                var subForum = new Forum
                {
                    Url = "sub",
                    ExternalUrl = null,
                    Description = "Sub forum",
                    PRI = 0,
                    IsNoTopic = false,
                    ParentId = defaultForum.Id,
                    IconId = null,
                    Password = null,
                    Title = LocalizationManager.c("Sub Template"),
                    Performance = ForumPerformance.Horizontal,
                    Highlight = null
                };
                DB.Forums.Add(subForum);
                DB.SaveChanges();
			}
		} 
	}
}