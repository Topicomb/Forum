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
	}
}