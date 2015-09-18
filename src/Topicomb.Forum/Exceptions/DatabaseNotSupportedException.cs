using System;

namespace Topicomb.Forum.Exceptions
{
	public class DatabaseNotSupportedException : Exception
	{
		public DatabaseNotSupportedException(string DbType)
			: base(DbType + "Not Supported")
		{
			
		}
	}
}