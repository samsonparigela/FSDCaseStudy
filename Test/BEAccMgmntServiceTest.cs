using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Services;
using NUnit.Framework;
using MavericksBank.Contexts;
using Microsoft.EntityFrameworkCore;
using MavericksBank.Repository;
using MavericksBank.Exceptions;

namespace MavericksBankTest
{
    [TestFixture]
    public class BankEmpAccMgmntServiceTest
    {
        RequestTrackerContext context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RequestTrackerContext>().UseInMemoryDatabase("dummy2Database").Options;
            context = new RequestTrackerContext(options);
        }

        [Test]
        public async Task ApproveAccountClosing()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmpAccMngmntService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);

            IBankEmpAccMngmtService service = new BankEmpAccMngmntService(_mockServicelogger.Object,_AccRepo,_TransacRepo,
                _CustRepo);

            var acc = new Accounts
            {
                AccountNumber = 22456,
                CustomerID = 1,
                Status = "Pending",
                AccountType = "Savings",
                Balance = 10000,
                IFSCCode = "SBI1",
            };
            await _AccRepo.Add(acc);
            var account = await service.ApproveAccountOpening(22456);
            Assert.IsNotNull(account);
            await _AccRepo.Delete(acc.AccountNumber);
            
        }

        [Test]
        public async Task GetCustomerDetailsforAccount()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmpAccMngmntService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);

            IBankEmpAccMngmtService service = new BankEmpAccMngmntService(_mockServicelogger.Object, _AccRepo, _TransacRepo,
                _CustRepo);

            var acc = new Accounts
            {
                AccountNumber = 45456,
                CustomerID = 1,
                Status = "Close Request",
                AccountType = "Savings",
                Balance = 10000,
                IFSCCode = "SBI1",
            };
            await _AccRepo.Add(acc);

            var customer = new Customer
            {
                CustomerID = 1,
                Name="Samson",
                UserID = 2,
                Aadhaar = "123",
                Gender = "Male",
                Age = 22,
                DOB=DateTime.Now,
                Address="HYD",
                Phone="9011",
                PANNumber="DDD"
            };
            await _CustRepo.Add(customer);
            var cust = await service.GetCustomerDetailsforAccount(45456);

            Assert.That(cust.Name=="Samson");
            await _AccRepo.Delete(acc.AccountNumber);
            await _CustRepo.Delete(customer.CustomerID);

        }

        [Test]
        public async Task ApproveAccountOpening()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmpAccMngmntService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);

            IBankEmpAccMngmtService service = new BankEmpAccMngmntService(_mockServicelogger.Object, _AccRepo, _TransacRepo,
                _CustRepo);

            var acc = new Accounts
            {
                AccountNumber = 22456,
                CustomerID = 1,
                Status = "Close Request",
                AccountType = "Savings",
                Balance = 10000,
                IFSCCode = "SBI1",
            };
            await _AccRepo.Add(acc);
            var account = await service.ApproveAccountClosing(22456);
            Assert.That(account.Status== "Account Closing Approved");
            await _AccRepo.Delete(acc.AccountNumber);

        }

        [Test]
        public async Task GetAllAccounts()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmpAccMngmntService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);

            IBankEmpAccMngmtService service = new BankEmpAccMngmntService(_mockServicelogger.Object, _AccRepo, _TransacRepo,
                _CustRepo);

            var acc = new Accounts
            {
                AccountNumber = 49656,
                CustomerID = 1,
                Status = "Approved",
                AccountType = "Savings",
                Balance = 10000,
                IFSCCode = "SBI1",
            };
            await _AccRepo.Add(acc);
            var accounts = await service.GetAllAccounts();
            Console.WriteLine(accounts.Count() + "Ramayyyaa");
            Assert.That(accounts.Count() == 1);
        }

        [Test]
        public async Task GetAllAccountsForCloseRequest()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmpAccMngmntService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);

            IBankEmpAccMngmtService service = new BankEmpAccMngmntService(_mockServicelogger.Object, _AccRepo, _TransacRepo,
                _CustRepo);

            var acc = new Accounts
            {
                AccountNumber = 49456,
                CustomerID = 1,
                Status = "Close Request",
                AccountType = "Savings",
                Balance = 10000,
                IFSCCode = "SBI1",
            };
            await _AccRepo.Add(acc);

            var accounts = await service.GetAllAccountsForCloseRequest();

            Assert.That(accounts.Count()==1);
            //await _AccRepo.Delete(acc.AccountNumber);
        }

        [Test]
        public async Task GetAllAccountsForOpenRequest()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmpAccMngmntService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);

            IBankEmpAccMngmtService service = new BankEmpAccMngmntService(_mockServicelogger.Object, _AccRepo, _TransacRepo,
                _CustRepo);

            var acc = new Accounts
            {
                AccountNumber = 669456,
                CustomerID = 1,
                Status = "Pending",
                AccountType = "Savings",
                Balance = 10000,
                IFSCCode = "SBI1",
            };
            await _AccRepo.Add(acc);

            var accounts = await service.GetAllAccountsForOpenRequest();

            Assert.That(accounts.Count() == 1);
        }

        [Test]
        public async Task GetAllTransactions()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmpAccMngmntService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);

            IBankEmpAccMngmtService service = new BankEmpAccMngmntService(_mockServicelogger.Object, _AccRepo, _TransacRepo,
                _CustRepo);
            List<Transactions> transacs = new List<Transactions>
        {
            new Transactions
            {
                TransactionDate=DateTime.Now,
                Amount=3000,
                BeneficiaryAccountNumber=12345,
                SAccountID=12345,
                Status="Successful",
                Description="Self Deposit",
                TransactionID=1,
                TransactionType="Deposit"
            },
            new Transactions
            {
                TransactionDate=DateTime.Now,
                Amount=4000,
                BeneficiaryAccountNumber=12345,
                SAccountID=23456,
                Status="Successful",
                Description="Withdraw",
                TransactionID=2,
                TransactionType="Withdraw"
            }
        };

            foreach (Transactions tr in transacs)
            {
                await _TransacRepo.Add(tr);
            }
            var transactions = await service.GetAllTransactions();
            Assert.That(transactions.Count() == 2);
            foreach (Transactions tr in transacs)
            {
                await _TransacRepo.Delete(tr.TransactionID);
            }
        }

        [Test]
        public async Task ViewAccountDetails()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmpAccMngmntService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);

            IBankEmpAccMngmtService service = new BankEmpAccMngmntService(_mockServicelogger.Object, _AccRepo, _TransacRepo,
                _CustRepo);

            var acc = new Accounts
            {
                AccountNumber = 49656,
                CustomerID = 1,
                Status = "Approved",
                AccountType = "Savings",
                Balance = 10000,
                IFSCCode = "SBI1",
            };
            await _AccRepo.Add(acc);
            var account = await service.ViewAccountDetails(49656);
            Assert.IsNotNull(account);
            await _AccRepo.Delete(acc.AccountNumber);
        }

        [Test]
        public async Task ViewTransactionDetailsByAccount()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmpAccMngmntService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);

            IBankEmpAccMngmtService service = new BankEmpAccMngmntService(_mockServicelogger.Object, _AccRepo, _TransacRepo,
                _CustRepo);
            List<Transactions> transacs = new List<Transactions>
        {
            new Transactions
            {
                TransactionDate=DateTime.Now,
                Amount=3000,
                BeneficiaryAccountNumber=12345,
                SAccountID=12345,
                Status="Successful",
                Description="Self Deposit",
                TransactionID=1,
                TransactionType="Deposit"
            },
            new Transactions
            {
                TransactionDate=DateTime.Now,
                Amount=4000,
                BeneficiaryAccountNumber=12345,
                SAccountID=23456,
                Status="Successful",
                Description="Withdraw",
                TransactionID=2,
                TransactionType="Withdraw"
            }
        };

            foreach (Transactions tr in transacs)
            {
                await _TransacRepo.Add(tr);
            }
            var transactions = await service.ViewTransactionDetailsByAccount(12345);
            Assert.That(transactions.Count() == 1);
            foreach (Transactions tr in transacs)
            {
                await _TransacRepo.Delete(tr.TransactionID);
            }
        }

        [Test]
        public async Task ViewSentTransactions()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmpAccMngmntService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);

            IBankEmpAccMngmtService service = new BankEmpAccMngmntService(_mockServicelogger.Object, _AccRepo, _TransacRepo,
                _CustRepo);
            List<Transactions> transacs = new List<Transactions>
        {
            new Transactions
            {
                TransactionDate=DateTime.Now,
                Amount=3000,
                BeneficiaryAccountNumber=12345,
                SAccountID=125545,
                Status="Successful",
                Description="Deposit",
                TransactionID=1,
                TransactionType="Deposit"
            },
            new Transactions
            {
                TransactionDate=DateTime.Now,
                Amount=4000,
                BeneficiaryAccountNumber=12345,
                SAccountID=255456,
                Status="Successful",
                Description="Withdraw",
                TransactionID=2,
                TransactionType="Sent"
            }
        };

            foreach (Transactions tr in transacs)
            {
                await _TransacRepo.Add(tr);
            }
            var transactions = await service.ViewSentTransactions(255456);
            Assert.That(transactions.Count() == 1);
            foreach (Transactions tr in transacs)
            {
                await _TransacRepo.Delete(tr.TransactionID);
            }
        }

        [Test]
        public async Task ViewReceivedTransactions()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmpAccMngmntService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);

            IBankEmpAccMngmtService service = new BankEmpAccMngmntService(_mockServicelogger.Object, _AccRepo, _TransacRepo,
                _CustRepo);
            List<Transactions> transacs = new List<Transactions>
        {
            new Transactions
            {
                TransactionDate=DateTime.Now,
                Amount=3000,
                BeneficiaryAccountNumber=12345,
                SAccountID=66666,
                Status="Successful",
                Description="Deposit",
                TransactionID=1,
                TransactionType="Deposit"
            },
            new Transactions
            {
                TransactionDate=DateTime.Now,
                Amount=4000,
                BeneficiaryAccountNumber=12345,
                SAccountID=11111,
                Status="Successful",
                Description="Withdraw",
                TransactionID=2,
                TransactionType="Sent"
            }
        };

            foreach (Transactions tr in transacs)
            {
                await _TransacRepo.Add(tr);
            }
            var transactions = await service.ViewReceivedTransactions(66666);
            Assert.That(transactions.Count() == 1);
            foreach (Transactions tr in transacs)
            {
                await _TransacRepo.Delete(tr.TransactionID);
            }

        }

        [Test]
        public async Task ViewTransactionsWith5HighestAmount()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmpAccMngmntService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);

            IBankEmpAccMngmtService service = new BankEmpAccMngmntService(_mockServicelogger.Object, _AccRepo, _TransacRepo,
                _CustRepo);
            List<Transactions> transacs = new List<Transactions>
        {
            new Transactions
            {
                TransactionDate=DateTime.Now,
                Amount=3000,
                BeneficiaryAccountNumber=12345,
                SAccountID=125545,
                Status="Successful",
                Description="Deposit",
                TransactionID=1,
                TransactionType="Deposit"
            },
            new Transactions
            {
                TransactionDate=DateTime.Now,
                Amount=4000,
                BeneficiaryAccountNumber=12345,
                SAccountID=255456,
                Status="Successful",
                Description="Withdraw",
                TransactionID=2,
                TransactionType="Sent"
            }
        };

            foreach (Transactions tr in transacs)
            {
                await _TransacRepo.Add(tr);
            }
            var transactions = await service.ViewTransactionsWith5HighestAmount();
            Assert.That(transactions.Count() == 2);
            foreach (Transactions tr in transacs)
            {
                await _TransacRepo.Delete(tr.TransactionID);
            }
        }
    }
}
