using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Topicomb.Forum.Models
{
	public class VoteRecord
	{
		public Guid Id { get; set; }
		
		[ForeignKey("VoteOption")]
		public Guid OptionId { get; set; }
		
		public virtual VoteOption VoteOption { get; set; }
		
		[ForeignKey("User")]
		public long UserId { get; set; }
		
		public virtual User User { get; set; }
		
		public DateTime Time { get; set; }
	}
}