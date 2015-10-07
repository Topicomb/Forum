using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.Entity;

namespace Topicomb.Forum.Models
{
	public class TopicTag
	{
		[ForeignKey("Topic")]
		public long TopicId { get; set; }
		
		public virtual Topic Topic { get; set; }
		
		[ForeignKey("ForumTag")]
		public Guid ForumTagId { get; set; }
		
		public ForumTag ForumTag { get; set; }
	}
}