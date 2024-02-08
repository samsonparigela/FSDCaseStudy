using System;
namespace MavericksBank.Exceptions
{
	public class NoBankEmployeeFoundException:Exception
	{
        string _message;
        public NoBankEmployeeFoundException()
        {
            _message = "No Bank Employees Found! Try Again";
        }
        public override string Message => _message;
    }
}

