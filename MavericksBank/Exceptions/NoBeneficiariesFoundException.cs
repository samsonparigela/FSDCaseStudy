using System;
namespace MavericksBank.Exceptions
{
	public class NoBeneficiariesFoundException:Exception
	{
        string _message;
        public NoBeneficiariesFoundException()
        {
            _message = "No Beneficiaries Found! Try Again";
        }
        public override string Message => _message;
    }
}

