using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Topicomb.Forum.Models
{
	public class Blob
	{
		public Guid Id { get; set; } 
		
		[MaxLength(256)]
		public string FileName { get; set; }
		
		public DateTime Time { get; set; }
		
		public string ContentType { get; set; }
		
		public long ContentLength { get; set; }
		
		[ForeignKey("User")]
		public long UserId { get; set; }
		
		public virtual User User { get; set; }
		
		[ForeignKey("Folder")]
		public Guid? FolderId { get; set; }
		
		public virtual Folder Folder { get; set; }
	}
}