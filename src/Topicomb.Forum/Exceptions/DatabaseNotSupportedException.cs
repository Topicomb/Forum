using System;

namespace Topicomb.Forum.Exceptions
{
	public class DatabaseNotSupportedException : NotSupportedException
	{
		public DatabaseNotSupportedException(string DbType)
			: base(DbType + "Not Supported")
		{
		}
	}
}