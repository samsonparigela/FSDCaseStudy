using System;
using System.ComponentModel.DataAnnotations;

namespace MavericksBank.Models
{
	public class Users:IEquatable<Users>
	{
        public Users()
        {

        }
        public Users(int userID, string userName, byte[] password, string userType)
        {
            UserID = userID;
            UserName = userName;
            Password = password;
            UserType = userType;
        }

        [Key]
        public int UserID { set; get; }
        public string UserName { set; get; }
        public byte[] Password { set; get; }
        public string UserType { set; get; }
        public byte[] Key { set; get; }

        public List<Admin> Admins { set; get; }
        public List<Customer> Customers { set; get; }
        public List<BankEmployee> BankEmployees { set; get; }

        public bool Equals(Users? other)
        {
            return this.UserID == other.UserID;
        }

        public override string ToString()
        {
            return $"UserID : {UserID}\nUserName : {UserName}\nPassword : {Password}\nUserType : {UserType}";
        }
    }
}

