using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MavericksBank.Models
{
	public class Customer:IEquatable<Customer>
	{
        public Customer()
        {

        }
        public Customer(int customerID, int userID, string name,
            string gender, string phone, string address, DateTime dOB, string aadhaar,
            string pANNumber, int age)
        {
            CustomerID = customerID;
            Name = name;
            Gender = gender;
            Phone = phone;
            Address = address;
            DOB = dOB;
            Aadhaar = aadhaar;
            PANNumber = pANNumber;
            Age = age;
            UserID = userID;
        }

        [Key]
        public int CustomerID { set; get; }

        public int UserID { set; get; }
        [ForeignKey("UserID")]

        public Users? Users { set; get; }

        public string Name { set; get; }
        public DateTime DOB { set; get; }
        public int Age { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public string Gender { set; get; }
        public string Aadhaar { set; get; }
        public string PANNumber { set; get; }

        //Navigations

        public List<Accounts> Accounts { set; get; }
        public List<Loan> Loans { set; get; }
        public List<Beneficiaries> Beneficiaries { set; get; }


        public bool Equals(Customer? other)
        {
            return other.CustomerID == this.CustomerID;
        }

        public override string ToString()
        {
            return $"CustomerID : {CustomerID}\nName : {Name}\nGender : {Gender}\nPhone : {Phone}\n" +
                $"Address : {Address}\nDOB : {DOB}\n" +
                $"Aadhar Number : {Aadhaar}\nPAN Number : {PANNumber}\nAge : {Age}";
        }
    }
}

