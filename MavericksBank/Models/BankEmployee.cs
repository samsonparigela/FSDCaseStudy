using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models
{
	public class BankEmployee:IEquatable<BankEmployee>
	{
        public BankEmployee(int employeeID, string name, string position, int userID)
        {
            EmployeeID = employeeID;
            Name = name;
            UserID = userID;
            Position = position;
        }

        [Key]
        public int EmployeeID { set; get; }
        public string Name { set; get; }

        [ForeignKey("UserID")]
        public int UserID { set; get; }

        public Users Users { set; get; }
        public string Position { set; get; }

        public bool Equals(BankEmployee? other)
        {
            return this.EmployeeID == other.EmployeeID;
        }

        public override string ToString()
        {
            return $"EmployeeID : {EmployeeID}\nName : {Name}\nUserID : {Users.UserID}\nPosition : {Position}";
        }
    }
}

