using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace Topicomb.Forum.Models
{
	public class ForumContext : IdentityDbContext
	{
		public DbSet<Blob> Blobs { get; set; }
		public DbSet<Forum> Forums { get; set; }
		public DbSet<Topic> Topics { get; set; }
		public DbSet<VoteOption> VoteOptions { get; set; }
		public DbSet<VoteRecord> VoteRecords { get; set; }
		
		protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
			
			builder.Entity<Blob> (e =>
			{
				e.Index(x => x.Time);
				e.Index(x => x.FileName);
			});
			
			builder.Entity<Forum> (e => 
			{
				e.Index(x => x.Url).Unique();
				e.Index(x => x.PRI);
			});
			
			builder.Entity<Topic> (e => 
			{
				e.Index(x => x.CreationTime);
				e.Index(x => x.LastReplyTime);
				e.Index(x => x.TopArea);
				e.Index(x => x.TopPRI);
			});
			
			builder.Entity<VoteOption> (e =>
			{
				e.Index(x => x.PRI);
				e.Index(x => x.Count);
			});
		}
	}
}
