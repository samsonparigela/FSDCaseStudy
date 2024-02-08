using System;
namespace MavericksBank.Exceptions
{
	public class NoBankFoundException:Exception
	{
        string _message;
        public NoBankFoundException()
        {
            _message = "No Banks Found! Try Again";
        }
        public override string Message => _message;
    }
}

