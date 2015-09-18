using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Topicomb.Forum.Models
{
	public class ForumTag
	{
		public Guid Id { get; set; }
		
		[MaxLength(64)]
		public string Title { get; set; }
		
		public int Count { get; set; }
		
		[ForeignKey("Forum")]
		public long ForumId { get; set; }
		
		public virtual Forum Forum { get; set; }
		
		public int PRI { get; set; }
	}
}