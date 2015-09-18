using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace Topicomb.Forum.Models
{
	public class ForumContext : IdentityDbContext
	{
		public DbSet<Blob> Blobs { get; set; }
		public DbSet<Forum> Forums { get; set; }
		
		protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
			
			builder.Entity<Blob> (e =>
			{
				e.Index(x => x.Time);
				e.Index(x => x.FileName);
			});
		}
	}
}
