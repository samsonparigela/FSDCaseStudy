using System.Security.Cryptography;
using Azure.Core;
using MavericksBank.Contexts;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Repository;
using MavericksBank.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Azure.Core;

namespace MavericksBankTest;

public class CustomerTransactionServiceTest
{
    RequestTrackerContext context;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<RequestTrackerContext>().UseInMemoryDatabase("dummy2Database").Options;
        context = new RequestTrackerContext(options);
    }

    [Test]
    [Order(1)]
    public async Task AddAccountTest()
    {
        var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
        var _mockServicelogger = new Mock<ILogger<CustomerTransactionService>>();
        var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();

        IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
        IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
        ICustomerTransactionService service = new CustomerTransactionService(_mockServicelogger.Object, _TransacRepo, _AccRepo);

        List<Accounts> accounts = new List<Accounts>
        {
            new Accounts
            {
                AccountNumber=12345,
                CustomerID=1,
                Status="Approved",
                AccountType="Savings",
                Balance=5000,
                IFSCCode="SBI1",
            },
            new Accounts
            {
                AccountNumber=23456,
                CustomerID=1,
                Status="Approved",
                AccountType="Savings",
                Balance=10000,
                IFSCCode="SBI1",
            },
             new Accounts
            {
                AccountNumber=22456,
                CustomerID=1,
                Status="Pending",
                AccountType="Savings",
                Balance=10000,
                IFSCCode="SBI1",
            },
             new Accounts
            {
                AccountNumber=21456,
                CustomerID=1,
                Status="Account Closing Approve6",
                AccountType="Savings",
                Balance=10000,
                IFSCCode="SBI1",
            },


        };

        foreach (Accounts acc in accounts)
        {
            await _AccRepo.Add(acc);
            context.SaveChanges();
        }
        var count = await _AccRepo.GetAll();
        Console.WriteLine(count + "GGGGGGG");
        Assert.That(6 == count.Count());
    }

    [Test]
    [Order(2)]
    public async Task AddTransactionTest()
    {
        var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
        var _mockServicelogger = new Mock<ILogger<CustomerTransactionService>>();
        var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();

        IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
        IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
        ICustomerTransactionService service = new CustomerTransactionService(_mockServicelogger.Object, _TransacRepo, _AccRepo);

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
            _TransacRepo.Add(tr);
        }
        var transactions = await _TransacRepo.GetAll();
        Assert.That(transactions.Count==3);
    }


    [Test]
    [Order(3)]
    public async Task GetAllTransactionsTest()
    {
        //Arrange

        var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
        var _mockServicelogger = new Mock<ILogger<CustomerTransactionService>>();
        var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();

        IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
        IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
        ICustomerTransactionService service = new CustomerTransactionService(_mockServicelogger.Object, _TransacRepo, _AccRepo);


        var transactions = await service.GetAllTransactions(12345);
        Console.WriteLine(transactions.Count());
        //Assert

        Assert.That( 0== transactions.Count());

    }

    [Test]
    [Order(4)]
    public async Task WithdrawMoneyTest()
    {
        var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
        var _mockServicelogger = new Mock<ILogger<CustomerTransactionService>>();
        var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();

        IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
        IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
        ICustomerTransactionService service = new CustomerTransactionService(_mockServicelogger.Object, _TransacRepo, _AccRepo);

        var transaction = await service.WithdrawMoney(3000, 12345);
        context.SaveChanges();
        var account = await _AccRepo.GetByID(12345);
       Assert.That(account.Balance==2000);
    }

    [Test]
    [Order(5)]
    [TestCase(12345, 1000)]
    public async Task TransferMoneyTest(int accNumber, int amount)
    {
        var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
        var _mockServicelogger = new Mock<ILogger<CustomerTransactionService>>();
        var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();

        IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
        IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
        ICustomerTransactionService service = new CustomerTransactionService(_mockServicelogger.Object, _TransacRepo, _AccRepo);

        var transac = await service.TransferMoney(1000, 23456, 12345);
        context.SaveChanges();
        var account = await _AccRepo.GetByID(12345);
        Assert.That(account.Balance==1000);
    }

    [Test]
    [Order(6)]
    public async Task DepositMoneyTest()
    {
        var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
        var _mockServicelogger = new Mock<ILogger<CustomerTransactionService>>();
        var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();

        IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
        IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
        ICustomerTransactionService service = new CustomerTransactionService(_mockServicelogger.Object, _TransacRepo, _AccRepo);

        var transac = await service.DepositMoney(12345, 1000);
        var account = await _AccRepo.GetByID(12345);
        Assert.That(account.Balance == 2000);
    }

    [Test]
    [Order(7)]
    public async Task GetTransactionsByIDTest()
    {
        var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
        var _mockServicelogger = new Mock<ILogger<CustomerTransactionService>>();
        var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();

        IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
        IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
        ICustomerTransactionService service = new CustomerTransactionService(_mockServicelogger.Object, _TransacRepo, _AccRepo);

        var transac = await service.GetTransactionsByID(1);
        Assert.That(transac.TransactionID==1);
    }

    [Test]
    [Order(7)]
    public async Task GetByAccountNumber()
    {
        var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
        var _mockServicelogger = new Mock<ILogger<CustomerTransactionService>>();
        var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();

        IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
        IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
        ICustomerTransactionService service = new CustomerTransactionService(_mockServicelogger.Object, _TransacRepo, _AccRepo);

        var account = await _AccRepo.GetByID(12345);
        Assert.That(account.AccountNumber == 12345);
    }
}
