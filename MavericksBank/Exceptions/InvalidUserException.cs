using System;
namespace MavericksBank.Exceptions
{
	public class InvalidUserException:Exception
	{
        string _message;
        public InvalidUserException()
        {
            _message = "No User Found! Try Again";
        }
        public override string Message => _message;
    }
}

