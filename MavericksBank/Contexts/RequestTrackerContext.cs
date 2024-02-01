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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>()
                .HasKey(p => p.AccountID);
            modelBuilder.Entity<Admin>()
                .HasKey(p => p.AdminID);
            modelBuilder.Entity<BankEmployee>()
                .HasKey(p => p.EmployeeID);
            modelBuilder.Entity<Beneficiaries>()
                .HasKey(p => p.BeneficiaryID);
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

            //modelBuilder.Entity<Accounts>()
            //    .HasOne(p => p.Customer)
            //    .WithMany(a => a.Accounts)
            //    .HasForeignKey("Accounts","CustomerID");

            //modelBuilder.Entity<Accounts>()
            //    .HasOne(p => p.Banks)
            //    .WithMany(a => a.Accounts)
            //    .HasForeignKey("Accounts", "BankID");

            //modelBuilder.Entity<Accounts>()
            //    .HasOne(p => p.Beneficiaries)
            //    .WithOne(a => a.Accounts)
            //    .HasForeignKey("Accounts", "BeneficiaryID");

            //modelBuilder.Entity<Loan>()
            //    .HasOne(p => p.Customer)
            //    .WithMany(a => a.Loans)
            //    .HasForeignKey("Loan", "CustomerID");

            ////modelBuilder.Entity<Branches>()
            ////    .HasOne(p => p.Banks)
            ////    .WithMany(a => a.Branches)
            ////    .HasForeignKey("Branches", "BankID");
            ////    //.HasPrincipalKey(a => a.BankID);

            //modelBuilder.Entity<Beneficiaries>()
            //    .HasOne(p => p.Customer)
            //    .WithMany(a => a.Beneficiaries)
            //    .HasForeignKey("Beneficiaries", "CustomerID");

            //modelBuilder.Entity<Beneficiaries>()
            //    .HasOne(p => p.Accounts)
            //    .WithOne(a => a.Beneficiaries)
            //    .HasForeignKey("Beneficiaries", "AccountID");

            //modelBuilder.Entity<Transactions>()
            //    .HasOne(p => p.SourceAccount)
            //    .WithOne(a => a.SentTransactions)
            //    .HasForeignKey("Transactions", "SAccountID");

            //modelBuilder.Entity<Transactions>()
            //    .HasOne(p => p.DestinationAccount)
            //    .WithOne(a => a.RecievedTransactions)
            //    .HasForeignKey("Transactions", "DAccountID");

            //modelBuilder.Entity<Customer>()
            //    .HasOne(p => p.Users)
            //    .WithOne(a => a.Customer)
            //    .HasForeignKey("Customer", "UserID");

            //modelBuilder.Entity<BankEmployee>()
            //    .HasOne(p => p.Users)
            //    .WithOne(a => a.BankEmployee)
            //    .HasForeignKey("BankEmployee", "UserID");

            //modelBuilder.Entity<Admin>()
            //    .HasOne(p => p.Users)
            //    .WithOne(a => a.Admin)
            //    .HasForeignKey("Admin", "UserID");



        }
    }
}
