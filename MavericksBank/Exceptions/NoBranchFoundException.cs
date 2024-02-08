using System;
namespace MavericksBank.Exceptions
{
	public class NoBranchFoundException:Exception
	{
        string _message;
        public NoBranchFoundException()
        {
            _message = "No Branch Found! Try Again";
        }
        public override string Message => _message;
    }
}

