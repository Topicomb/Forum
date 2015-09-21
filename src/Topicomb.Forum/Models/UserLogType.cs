namespace Topicomb.Forum.Models
{
	public enum UserLogType
	{
		SignIn,
		SignOut,
		ResetPassword,
		ForgotPassword,
		CreditsTransfer,
		CreditIncome,
		RemoveTopic,
		RemoveReply,
		RemoveFriend,
		BlockFriend,
		AcceptFriend,
		SetTopicAsTop,
		SetTopicAsDigest,
		SetTopicHighlight,
		CommentWithCredits
	}
}