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

namespace MavericksBankTest
{
    [TestFixture]
    public class BankEmpAccMngmntServiceTests
    {
        private BankEmpAccMngmntService _bankEmpAccMngmntService;
        private Mock<IRepository<Accounts, int>> _accountRepositoryMock;
        private Mock<IRepository<Transactions, int>> _transactionRepositoryMock;
        private Mock<IRepository<Customer, int>> _customerRepositoryMock;
        private Mock<ILogger<BankEmpAccMngmntService>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _accountRepositoryMock = new Mock<IRepository<Accounts, int>>();
            _transactionRepositoryMock = new Mock<IRepository<Transactions, int>>();
            _customerRepositoryMock = new Mock<IRepository<Customer, int>>();
            _loggerMock = new Mock<ILogger<BankEmpAccMngmntService>>();

            _bankEmpAccMngmntService = new BankEmpAccMngmntService(
                _loggerMock.Object,
                _accountRepositoryMock.Object,
                _transactionRepositoryMock.Object,
                _customerRepositoryMock.Object
            );
        }

        [Test]
        public async Task ApproveAccountClosing()
        {
            // Arrange
            var accountId = 1;
            var account = new Accounts { AccountNumber = accountId, Status = "Pending" };

            _accountRepositoryMock.Setup(repo => repo.GetByID(accountId)).ReturnsAsync(account);

            // Act
            var result = await _bankEmpAccMngmntService.ApproveAccountClosing(accountId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Account Closing Approved", result.Status);
        }

        [Test]
        public async Task GetCustomerDetailsforAccount()
        {
            // Arrange
            var accountId = 1;
            var account = new Accounts { AccountNumber = accountId, CustomerID = 1 };
            var customer = new Customer { CustomerID = 1, Name = "John Doe", Address = "123 Main St", Phone = "555-5555" };

            _accountRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Accounts> { account });
            _customerRepositoryMock.Setup(repo => repo.GetByID(account.CustomerID)).ReturnsAsync(customer);

            // Act
            var result = await _bankEmpAccMngmntService.GetCustomerDetailsforAccount(accountId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(customer.CustomerID, result.CustomerID);
            Assert.AreEqual(customer.Name, result.Name);
            Assert.AreEqual(customer.Address, result.Address);
            Assert.AreEqual(customer.Phone, result.Phone);
        }

        [Test]
        public async Task ApproveAccountOpening()
        {
            // Arrange
            var accountId = 1;
            var account = new Accounts { AccountNumber = accountId, Status = "Pending" };

            _accountRepositoryMock.Setup(repo => repo.GetByID(accountId)).ReturnsAsync(account);

            // Act
            var result = await _bankEmpAccMngmntService.ApproveAccountOpening(accountId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Approved", result.Status);
        }

        [Test]
        public async Task GetAllAccounts()
        {
            // Arrange
            var accounts = new List<Accounts>
            {
                new Accounts { AccountNumber = 1, Status = "Active" },
                new Accounts { AccountNumber = 2, Status = "Closed" },
                // Add more accounts as needed
            };

            _accountRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(accounts);

            // Act
            var result = await _bankEmpAccMngmntService.GetAllAccounts();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(accounts.Count, result.Count);
        }

        [Test]
        public async Task GetAllAccountsForCloseRequest()
        {
            // Arrange
            var accounts = new List<Accounts>
            {
                new Accounts { AccountNumber = 1, Status = "Close Request" },
                new Accounts { AccountNumber = 2, Status = "Active" },
                new Accounts { AccountNumber = 3, Status = "Close Request" },
            };

            _accountRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(accounts);

            // Act
            var result = await _bankEmpAccMngmntService.GetAllAccountsForCloseRequest();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetAllAccountsForOpenRequest()
        {
            // Arrange
            var accounts = new List<Accounts>
            {
                new Accounts { AccountNumber = 1, Status = "Pending" },
                new Accounts { AccountNumber = 2, Status = "Active" },
                new Accounts { AccountNumber = 3, Status = "Pending" },
            };

            _accountRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(accounts);

            // Act
            var result = await _bankEmpAccMngmntService.GetAllAccountsForOpenRequest();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetAllTransactions()
        {
            // Arrange
            var transactions = new List<Transactions>
            {
                new Transactions { TransactionID = 1, Amount = 100, TransactionType = "Deposit" },
                new Transactions { TransactionID = 2, Amount = 50, TransactionType = "Withdraw" },
                // Add more transactions as needed
            };

            _transactionRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(transactions);

            // Act
            var result = await _bankEmpAccMngmntService.GetAllTransactions();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(transactions.Count, result.Count);
            Assert.IsTrue(result.All(t => t.Amount >= 0));
        }

        [Test]
        public async Task ViewAccountDetails()
        {
            // Arrange
            var accountId = 1;
            var account = new Accounts { AccountNumber = accountId, Status = "Active" };

            _accountRepositoryMock.Setup(repo => repo.GetByID(accountId)).ReturnsAsync(account);

            // Act
            var result = await _bankEmpAccMngmntService.ViewAccountDetails(accountId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(account.AccountNumber, result.AccountNumber);
            Assert.AreEqual(account.Status, result.Status);
        }

        [Test]
        public async Task ViewTransactionDetailsByAccount()
        {
            // Arrange
            var accountId = 1;
            var transactions = new List<Transactions>
            {
                new Transactions { TransactionID = 1, SAccountID = accountId, Amount = 100, TransactionType = "Deposit" },
                new Transactions { TransactionID = 2, SAccountID = accountId, Amount = 50, TransactionType = "Withdraw" },
                // Add more transactions as needed
            };

            _transactionRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(transactions);

            // Act
            var result = await _bankEmpAccMngmntService.ViewTransactionDetailsByAccount(accountId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(transactions.Count, result.Count);
            Assert.IsTrue(result.All(t => t.Amount >= 0));
        }

        [Test]
        public async Task ViewSentTransactions()
        {
            // Arrange
            var accountId = 1;
            var transactions = new List<Transactions>
            {
                new Transactions { TransactionID = 1, SAccountID = accountId, Amount = 100, TransactionType = "Sent" },
                new Transactions { TransactionID = 2, SAccountID = accountId, Amount = 50, TransactionType = "Sent" },
                // Add more transactions as needed
            };

            _transactionRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(transactions);

            // Act
            var result = await _bankEmpAccMngmntService.ViewSentTransactions(accountId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(transactions.Count, result.Count);
            Assert.IsTrue(result.All(t => t.Amount >= 0)); 
            Assert.IsTrue(result.All(t => t.TransactionType == "Sent")); 
        }

        [Test]
        public async Task ViewReceivedTransactions()
        {
            // Arrange
            var accountId = 1;
            var transactions = new List<Transactions>
            {
                new Transactions { TransactionID = 1, SAccountID = accountId, Amount = 100, TransactionType = "Deposit" },
                new Transactions { TransactionID = 2, SAccountID = accountId, Amount = 50, TransactionType = "Withdraw" },
                // Add more transactions as needed
            };

            _transactionRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(transactions);

            // Act
            var result = await _bankEmpAccMngmntService.ViewReceivedTransactions(accountId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(transactions.Count, result.Count);
            Assert.IsTrue(result.All(t => t.Amount >= 0));
            Assert.IsTrue(result.All(t => t.TransactionType == "Deposit" || t.TransactionType == "Withdraw")); // Ensure all transactions are either "Deposit" or "Withdraw" type
        }

        [Test]
        public async Task ViewTransactionsWith5HighestAmount()
        {
            // Arrange
            var transactions = new List<Transactions>
            {
                new Transactions { TransactionID = 1, Amount = 100 },
                new Transactions { TransactionID = 2, Amount = 200 },
                new Transactions { TransactionID = 3, Amount = 150 },
                new Transactions { TransactionID = 4, Amount = 250 },
                new Transactions { TransactionID = 5, Amount = 50 },
            };

            _transactionRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(transactions);

            // Act
            var result = await _bankEmpAccMngmntService.ViewTransactionsWith5HighestAmount();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(250, result[0].Amount);
            Assert.IsTrue(result.All(t => t.Amount >= 0));
        }
    }
}
