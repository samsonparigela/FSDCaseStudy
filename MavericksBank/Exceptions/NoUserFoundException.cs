using System;
namespace MavericksBank.Exceptions
{
	public class NoUserFoundException : Exception
    {
        string _message;
        public NoUserFoundException()
        {
            _message = "No User Found! Try Again";
        }
        public override string Message => _message;
    }
}

