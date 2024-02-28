using System;
namespace MavericksBank.Exceptions
{
	public class AccountAlreadyPresentException:Exception
	{
        string _message;
        public AccountAlreadyPresentException(string message)
        {
            _message = message;
        }
        public override string Message => _message;
    }
}

