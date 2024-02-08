using System;
namespace MavericksBank.Exceptions
{
	public class NoAdminFoundException:Exception
	{
        string _message;
        public NoAdminFoundException()
        {
            _message = "No Admins Found! Try Again";
        }
        public override string Message => _message;

    }
}

