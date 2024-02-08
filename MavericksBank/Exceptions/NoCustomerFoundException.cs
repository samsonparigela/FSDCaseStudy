using System;
namespace MavericksBank.Exceptions
{
	public class NoCustomerFoundException:Exception
	{
		string _message;
		public NoCustomerFoundException()
		{
			_message = "No Customers Found! Try Again";
		}
        public override string Message => _message;
    }
}

