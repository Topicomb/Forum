using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Topicomb.Forum.Models
{
	public class TopicEvaluate
	{
		public Guid Id { get; set; }
		
		[ForeignKey("User")]
		public long UserId { get; set; }
		
		public virtual User User { get; set; }
		
		public DateTime Time { get; set; }
		
		public virtual ICollection<TopicEvaluateDetail> Details { get; set; }
	}
}