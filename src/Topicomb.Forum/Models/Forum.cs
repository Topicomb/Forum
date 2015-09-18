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
		
		[ForeignKey("Parent")]
		public long ParentId { get; set; }
		
		public Forum Parent { get; set; }
		
		public string Password { get; set; }
		
		[ForeignKey("Blob")]
		public Guid IconId { get; set; }
		
		public int PRI { get; set; }
		
		public virtual Blob Icon { get; set; }
	}
}