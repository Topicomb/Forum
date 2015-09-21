using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Topicomb.Forum.Models
{
	public class UserLog
	{
		public Guid Id { get; set; }
		
		[ForeignKey("User")]
		public long UserId { get; set; }
		
		public virtual User User { get; set; }
		
		public DateTime Time { get; set; }
		
		public UserLogType Type { get; set; }
		
		public string Hint { get; set; }
	}
}