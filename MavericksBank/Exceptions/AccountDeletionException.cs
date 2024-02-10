using System;
namespace MavericksBank.Exceptions
{
	public class AccountDeletionException:Exception
	{
		string _message;
		public AccountDeletionException(string message)
		{
			_message = message;
		}
		public override string Message => _message;
    }
}

