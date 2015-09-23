using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
			
			if (DB.Database != null && await DB.Database.EnsureCreatedAsync())
			{
				// Creating Roles
				await RoleManager.CreateAsync(new IdentityRole<long> { Name = "Root" });
                await RoleManager.CreateAsync(new IdentityRole<long> { Name = "Super Moderator" });
				await RoleManager.CreateAsync(new IdentityRole<long> { Name = "Moderator" });
				await RoleManager.CreateAsync(new IdentityRole<long> { Name = "Member" });
				await RoleManager.CreateAsync(new IdentityRole<long> { Name = "Blocked" });
				
				// Creating Root User
				var user = new User { UserName = Startup.Configuration["Installation:RootUserName"], Email = "admin@codecomb.com" };
				var result2 = await UserManager.CreateAsync(user, Startup.Configuration["Installation:Password"]);
                foreach (var e in result2.Errors)
                    throw new Exception(e.Description);
				await UserManager.AddToRoleAsync(user, "Root");
			}
		} 
	}
}