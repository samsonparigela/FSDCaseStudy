using System;
namespace MavericksBank.Exceptions
{
	public class NoLoanFoundException : Exception
    {
        string _message;
        public NoLoanFoundException(string message)
        {
            _message = message;
        }
        public override string Message => _message;
    }
}

