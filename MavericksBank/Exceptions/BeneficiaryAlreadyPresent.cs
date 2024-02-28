using System;
namespace MavericksBank.Exceptions
{
	public class BeneficiaryAlreadyPresent:Exception
	{
        string _message;
        public BeneficiaryAlreadyPresent(string message)
        {
            _message = message;
        }
        public override string Message => _message;
    }
}

