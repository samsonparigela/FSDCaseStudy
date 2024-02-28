using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Emit;
using MavericksBank.Models;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Contexts
{
	public class RequestTrackerContext:DbContext
	{
		public RequestTrackerContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<BankEmployee> BankEmployee { get; set; }
        public DbSet<Beneficiaries> Beneficiaries { get; set; }
        public DbSet<Banks> Banks { get; set; }
        public DbSet<Branches> Branches { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<LoanPolicies> LoanPolicies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>()
                .HasKey(p => p.AccountNumber);
            modelBuilder.Entity<Admin>()
                .HasKey(p => p.AdminID);
            modelBuilder.Entity<BankEmployee>()
                .HasKey(p => p.EmployeeID);
            modelBuilder.Entity<Beneficiaries>()
                .HasKey(p => p.BeneficiaryAccountNumber);
            modelBuilder.Entity<Banks>()
                .HasKey(p => p.BankID);
            modelBuilder.Entity<Branches>()
                .HasKey(p => p.IFSCCode);
            modelBuilder.Entity<Customer>()
                .HasKey(p => p.CustomerID);
            modelBuilder.Entity<Loan>()
                .HasKey(p => p.LoanID);
            modelBuilder.Entity<Transactions>()
                .HasKey(p => p.TransactionID);
            modelBuilder.Entity<Users>()
                .HasKey(p => p.UserID);
            modelBuilder.Entity<LoanPolicies>()
                .HasKey(p => p.LoanPolicyID);

            modelBuilder.Entity<Accounts>()
                .HasOne(p => p.Customer)
                .WithMany(a => a.Accounts)
                .HasForeignKey("CustomerID");

            modelBuilder.Entity<Accounts>()
                .HasOne(p => p.Branches)
                .WithMany(a => a.Accounts)
                .HasForeignKey("IFSCCode");

            modelBuilder.Entity<Loan>()
                .HasOne(p => p.Customer)
                .WithMany(a => a.Loans)
                .HasForeignKey("CustomerID");

            modelBuilder.Entity<Loan>()
                .HasOne(p => p.LoanPolicies)
                .WithMany(a => a.Loans)
                .HasForeignKey("LoanPolicyID");

            modelBuilder.Entity<Branches>()
                .HasOne(p => p.Banks)
                .WithMany(a => a.Branches)
                .HasForeignKey("BankID");

            modelBuilder.Entity<Beneficiaries>()
                .HasOne(p => p.Customer)
                .WithMany(a => a.Beneficiaries)
                .HasForeignKey("CustomerID");

            modelBuilder.Entity<Beneficiaries>()
                .HasOne(p => p.Branch)
                .WithMany(a => a.Beneficiaries)
                .HasForeignKey("IFSCCode");

            modelBuilder.Entity<Transactions>()
                .HasOne(p => p.SourceAccount)
                .WithMany(a => a.RecievedTransactions)
                .HasForeignKey("SAccountID");

            modelBuilder.Entity<Transactions>()
                .HasOne(p => p.Beneficiaries)
                .WithMany(a => a.Transactions)
                .HasForeignKey("BeneficiaryID")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Customer>()
                .HasOne(p => p.Users)
                .WithMany(a => a.Customers)
                .HasForeignKey("UserID");

            modelBuilder.Entity<BankEmployee>()
                .HasOne(p => p.Users)
                .WithMany(a => a.BankEmployees)
                .HasForeignKey("UserID");

            modelBuilder.Entity<Admin>()
                .HasOne(p => p.Users)
                .WithMany(a => a.Admins)
                .HasForeignKey("UserID");


        }
    }
}
