using System;
namespace MavericksBank.Exceptions
{
	public class LoanNotApprovedYetException:Exception
	{
        string _message;
        public LoanNotApprovedYetException(string message)
        {
            _message = message;
        }
        public override string Message => _message;
    }
}

