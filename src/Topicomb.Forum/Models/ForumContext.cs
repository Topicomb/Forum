using System;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace Topicomb.Forum.Models
{
	public class ForumContext : IdentityDbContext<User, IdentityRole<long>, long>
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
		public DbSet<CreditLog> CreditLogs { get; set; }
		public DbSet<FriendRelation> FriendRelations { get; set; }
		public DbSet<Folder> Folders { get; set; }
		public DbSet<UserLog> UserLogs { get; set; }
		public DbSet<TopicEvaluate> TopicEvaluates { get; set; }
		public DbSet<TopicEvaluateDetail> TopicEvaluateDetials { get; set; }
		
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
				e.Index(x => x.DisplayInProfile);
				e.Index(x => x.PRI);
			});
			
			builder.Entity<CreditLog> (e => 
			{
				e.Index(x => x.Time);
				e.Index(x => x.Count);
			});
			
			builder.Entity<FriendRelation> (e =>
			{
				e.Index(x => x.Time);
				e.Index(x => x.Status);
				e.Key(x => new { x.FriendId, x.UserId });
			});
			
			builder.Entity<UserLog> (e => {
				e.Index(x => x.Type);
				e.Index(x => x.Time);
			});
			
			builder.Entity<TopicEvaluate> (e => 
			{
				e.Index(x => x.Time);
			});
		}
	}
}
