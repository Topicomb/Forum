using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Topicomb.Forum.Models
{
	public class Notification
	{
		public Guid Id { get; set; }
		
		public string Title { get; set; }
		
		public DateTime Time { get; set; }
		
		public string Content { get; set; }
		
		public long? UserId { get; set; }
		
		public virtual User User { get; set; }
	}
}