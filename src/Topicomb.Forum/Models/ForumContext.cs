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
		public DbSet<ForumTag> ForumTags { get; set; }
		public DbSet<TopicTag> TopicTags { get; set; }
		public DbSet<Link> Links { get; set; }
		public DbSet<PrivateMessage> PrivateMessages { get; set; }
		public DbSet<Credit> Credits { get; set; }
		
		protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(e =>
            {
                e.Collection(x => x.Sent)
                    .InverseReference(x => x.Sender);
                e.Collection(x => x.Received)
                    .InverseReference(x => x.Receiver);
            });

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
			
			builder.Entity<VoteRecord> (e => 
			{
				e.Index(x => x.Time);
			});
			
			builder.Entity<ForumTag> (e => 
			{
				e.Index(x => x.Title);
				e.Index(x => x.Count);
				e.Index(x => x.PRI);
			});
			
			builder.Entity<TopicTag> (e =>
			{
				e.Key(x => new { x.ForumTagId, x.TopicId });
			});
			
			builder.Entity<Link> (e =>
			{
				e.Index(x => x.PRI);
				e.Index(x => x.Type);
			});
			
			builder.Entity<PrivateMessage> (e => 
			{
				e.Index(x => x.Time);
				e.Index(x => x.IsRead);
			});
			
			builder.Entity<Credit> (e => 
			{
				e.Index(x => x.IsBlankedOut);
				e.Index(x => x.IsTransferable);
				e.Index(x => x.DisplayInProfile);
			});
		}
	}
}
