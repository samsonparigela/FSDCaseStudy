using System;
namespace MavericksBank.Exceptions
{
	public class InsufficientFundsException:Exception
	{
        string _message;
        public InsufficientFundsException(string message)
        {
            _message = message;
        }
        public override string Message => _message;
    }
}

