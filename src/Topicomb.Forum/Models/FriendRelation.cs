using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Topicomb.Forum.Models
{
	public class FriendRelation
	{
		[ForeignKey("User")]
		public long UserId { get; set; }
		
		public virtual User User { get; set; }
		
		[ForeignKey("Friend")]
		public long FriendId { get; set; }
		
		public string Hint { get; set; }
		
		public virtual User Friend { get; set; }
		
		public DateTime Time { get; set; }
		
		public FriendRelationStatus Status { get; set; }
	}
}