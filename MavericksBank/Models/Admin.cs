using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models
{
	public class Admin
	{
        public Admin(int adminID, string name,int userID)
        {
            AdminID = adminID;
            UserID = userID;
            Name = name;
        }

        [Key]
        public int AdminID { set; get; }

        [ForeignKey("UserID")]
        public int UserID { set; get; }

        public Users Users { set; get; }
        public string Name { set; get; }

        public override string ToString()
        {
            return $"AdminID : {AdminID}\nUserID : {Users.UserID}\nName : {Name}\n";
        }
    }
}

