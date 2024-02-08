using System;
namespace MavericksBank.Exceptions
{
	public class NoLoanFoundException : Exception
    {
        string _message;
        public NoLoanFoundException()
        {
            _message = "No Loan Found! Try Again";
        }
        public override string Message => _message;
    }
}

