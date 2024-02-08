using System;
namespace MavericksBank.Exceptions
{
	public class NoAccountFoundException:Exception
	{
        string _message;
        public NoAccountFoundException()
        {
            _message = "No Accounts Found! Try Again";
        }
        public override string Message => _message;
    }
}

