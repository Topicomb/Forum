using Microsoft.AspNet.Identity.EntityFramework;

namespace Topicomb.Forum.Models
{
	public class User : IdentityUser<int>
	{
		public byte[] Avatar { get; set; }
		
		public string AvatarContentType { get; set; }
		
		public string Motto { get; set; }
	}
}