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
				// TODO: Initial the database
			}
		} 
	}
}