using System;
namespace MavericksBank.Exceptions
{
	public class NoTransactionsFoundException : Exception
    {
        string _message;
        public NoTransactionsFoundException()
        {
            _message = "No Transactions Found! Try Again";
        }
        public override string Message => _message;
    }
}

