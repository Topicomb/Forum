using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Topicomb.Forum.Models
{
	public class Link
	{
		public long Id { get; set; }
		
		[MaxLength(128)]
		public string Title { get; set; }
		
		[ForeignKey("Icon")]
		public Guid IconId { get; set; }
		
		public virtual Blob Icon { get; set; }
		
		public int PRI { get; set; }
		
		[MaxLength(512)]
		public string URL { get; set; }
		
		public LinkType Type { get; set; }
	}
}