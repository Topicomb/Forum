using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Topicomb.Forum.Models
{
	public class TopicEvaluateDetail
	{
		public Guid Id { get; set; }
		
		[ForeignKey("CreditLog")]
		public Guid CreditLogId { get; set; }
		
		public CreditLog CreditLog { get; set; }
	}
}