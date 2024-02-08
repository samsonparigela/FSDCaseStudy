using System;
using System.Security.Cryptography;
using System.Text;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Mappers
{
	public class RegisterToUser
	{
		Users myUser;

        public RegisterToUser(CustomerRegisterDTO customerRegister)
		{
			myUser = new Users();
			myUser.UserName = customerRegister.UserName;
			myUser.UserType = customerRegister.UserType;
			getPassword(customerRegister.Password);
		}

		void getPassword(string password)
		{
            HMACSHA512 hmac = new HMACSHA512();
            myUser.Key = hmac.Key;
            myUser.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

		public Users GetUser()
		{
			return myUser;
		}
    }
}

