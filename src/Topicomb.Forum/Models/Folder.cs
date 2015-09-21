using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Topicomb.Forum.Models
{
	public class Folder
	{
		public Guid Id { get; set; }
		
		[ForeignKey("User")]
		public long UserId { get; set; }
		
		public virtual User User { get; set; }
		
		[MaxLength(64)]
		public string Title { get; set; }
		
		public string Description { get; set; }
		
		[MaxLength(64)]
		public string Password { get; set; }
		
		public DateTime Time { get; set; }
		
		public virtual ICollection<Blob> Files { get; set; } = new List<Blob> ();
	}
}