using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Topicomb.Forum.Models
{
	public class VoteOption
	{
		public Guid Id { get; set; }
		
		[MaxLength(128)]
		public string Title { get; set; }
		
		public int PRI { get; set; }
		
		public int Count { get; set; }
		
		[ForeignKey("Topic")]
		public long TopicId { get; set; }
		
		public virtual Topic Topic { get; set; }
		
		public virtual ICollection<VoteRecord> VoteRecords { get; set; }
	}
}