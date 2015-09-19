using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Topicomb.Forum.Models
{
	public class User : IdentityUser<long>
	{
		[ForeignKey("Avatar")]
		public Guid AvatarId { get; set; }
		
		public virtual Blob Avatar { get; set; }
		
		public string AvatarContentType { get; set; }
		
		public string Motto { get; set; }

        public virtual ICollection<PrivateMessage> Sent { get; set; }
        
        public virtual ICollection<PrivateMessage> Received { get; set; }
	}
}