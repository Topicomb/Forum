using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Topicomb.Forum.Models
{
	public class Forum
	{
		public long Id { get; set; }
		
		[MaxLength(32)]
		public string Url { get; set; }
		
		[MaxLength(128)]
		public string Title { get; set; }
		
		public string Description { get; set; }
		
		
	}
}