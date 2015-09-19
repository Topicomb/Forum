using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Topicomb.Forum.Models
{
	public class PrivateMessage
	{
		public Guid Id { get; set; }
		
		[ForeignKey("Sender")]
		public long SenderId { get; set; }
		
		public virtual User Sender { get; set; }
		
		[ForeignKey("Receiver")]
		public long ReceiverId { get; set; }
		
		public virtual User Receiver { get; set; }
		
		public DateTime Time { get; set; }
		
		public bool IsRead { get; set; }
		
		public string Content { get; set; }
	}
}