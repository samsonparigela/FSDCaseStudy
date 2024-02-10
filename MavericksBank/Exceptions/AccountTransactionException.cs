using System;
namespace MavericksBank.Exceptions
{
	public class AccountTransactionException:Exception
	{
        string _message;
        public AccountTransactionException(string message)
        {
            _message = message;
        }
        public override string Message => _message;
    }
}

