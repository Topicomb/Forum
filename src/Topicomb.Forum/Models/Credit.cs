using System;
using System.ComponentModel.DataAnnotations;

namespace Topicomb.Forum.Models
{
	public class Credit
	{
		public Guid Id { get; set; }
		
		[MaxLength(32)]
		public string Title { get; set; }
		
		[MaxLength(32)]
		public string Unit { get; set; }
		
		public bool DisplayInProfile { get; set; }
		
		public bool IsBlankedOut { get; set; }
		
		public bool IsTransferable { get; set; }
		
		public bool IsCommentable { get; set; }
		
		public double DayLimit { get; set; }
		
		public double SingleLimit { get; set; }
		
		public double CommentLimit { get; set; }
		
		public int PRI { get; set; }
	}
}