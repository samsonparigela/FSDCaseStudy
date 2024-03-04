using System;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using System.Security.Cryptography;
using System.Text;

namespace MavericksBank.Mappers
{
	public class RegisterToAdmin
	{
        Users myUser;

        public RegisterToAdmin(string username, string password)
        {
            myUser = new Users();
            myUser.UserName = username;
            myUser.UserType = "Admin";
            getPassword(password);
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

