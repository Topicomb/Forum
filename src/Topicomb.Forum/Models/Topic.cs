using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Topicomb.Forum.Models
{
	public class Topic
	{
		public long Id { get; set; }
		
		[MaxLength(128)]
		public string Title { get; set; }
		
		public TopicType Type { get; set; }
		
		public string Content { get; set; }
		
		public DateTime CreationTime { get; set; }
		
		public DateTime? LastReplyTime { get; set; }
		
		public bool IsMultiVote { get; set; }
		
		public int VoteDays { get; set; }
		
		public bool IsLocked { get; set; }
		
		public string Highlight { get; set; }
		
		public TopArea TopArea { get; set; }
		
		public int TopPRI { get; set; }
		
		public int Digest { get; set; }
		
		[ForeignKey("User")]
		public long UserId { get; set; }
		
		public virtual User User { get; set; }
		
		[ForeignKey("LastReplyUser")]
		public long? LastReplyUserId { get; set; }
		
		public virtual User LastReplyUser { get; set; }
		
		[ForeignKey("Parent")]
		public long? ParentId { get; set; }
		
		public virtual Topic Parent { get; set; }
		
		public virtual ICollection<VoteOption> VoteOptions { get; set; } = new List<VoteOption> ();
	
		public virtual ICollection<TopicEvaluate> Evaluates { get; set; } = new List<TopicEvaluate> ();
	}
}