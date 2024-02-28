using System;
namespace MavericksBank.Exceptions
{
	public class UserExistsException:Exception
	{
        string _message;
        public UserExistsException(string message)
        {
            _message = message;
        }
        public override string Message => _message;
    }
}

