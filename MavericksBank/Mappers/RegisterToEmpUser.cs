using System;
using System.Security.Cryptography;
using System.Text;
using MavericksBank.Models;
using MavericksBank.Models.DTO;

namespace MavericksBank.Mappers
{
	public class RegisterToEmpUser
	{
		Users myUser;
		public RegisterToEmpUser(EmpRegisterDTO empRegister)
		{
			myUser = new Users();
            myUser.UserName = empRegister.UserName;
            myUser.UserType = empRegister.UserType;
			GetEncryptedPassword(empRegister.Password);
		}

		public void GetEncryptedPassword(string password)
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

