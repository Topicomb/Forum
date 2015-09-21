using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Topicomb.Forum.Models
{
	public class CreditLog
	{
		public Guid Id { get; set; }
		
		public DateTime Time { get; set; }
		
		public double Count { get; set; }
		
		[ForeignKey("Credit")]
		public Guid CreditId { get; set; }
		
		public virtual Credit Credit { get; set; }
		
		[ForeignKey("User")]
		public long UserId { get; set; }
		
		public virtual User User { get; set; }
		
		public string Hint { get; set; }
	}
}