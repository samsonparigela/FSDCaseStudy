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
                SAccountID=12345,
                Status="Successful",
                Description="Withdraw",
                TransactionID=2,
                TransactionType="Withdraw"
            }
        };

        foreach(Transactions tr in transacs)
        {
            await _TransacRepo.Add(tr);
            context.SaveChanges();
        }
        var count = await _TransacRepo.GetAll();
        Assert.That(2 == count.Count());
    }


    [Test]
    public async Task GetAllTransactionsTest()
    {
        //Arrange

        var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
        var _mockServicelogger = new Mock<ILogger<CustomerTransactionService>>();
        var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();

        IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
        IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
        ICustomerTransactionService service = new CustomerTransactionService(_mockServicelogger.Object, _TransacRepo, _AccRepo);



        var trans = await _TransacRepo.Add(
            new Transactions
            {
                TransactionDate = DateTime.Now,
                Amount = 4000,
                BeneficiaryAccountNumber = 12345,
                SAccountID = 12345,
                Status = "Successful",
                Description = "Withdraw",
                TransactionID = 2,
                TransactionType = "Withdraw"
            }
        );

        //Action
        Console.WriteLine(trans);
        var transactions = await service.GetAllTransactions(12345);

        //Assert
        Console.WriteLine(transactions.Count());
        Assert.That(0==transactions.Count());
    }

    [Test]
    public void WithdrawMoneyTest()
    {
        Assert.Pass();
    }

    [Test]
    [TestCase(12345,1000)]
    public async Task TransferMoneyTest(int accNumber, int amount)
    {
        var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
        var _mockServicelogger = new Mock<ILogger<CustomerTransactionService>>();
        var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();

        IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
        IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
        ICustomerTransactionService service = new CustomerTransactionService(_mockServicelogger.Object, _TransacRepo, _AccRepo);

        var account = await service.WithdrawMoney(1000,12345);
        Console.WriteLine(account);
        Assert.Pass();
    }

    [Test]
    public void DepositMoneyTest()
    {
        Assert.Pass();
    }

    [Test]
    public void GetTransactionsByIDTest()
    {
        Assert.Pass();
    }

    [Test]
    [Order(1)]
    public async Task GetByAccountNumber()
    {
        var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
        var _mockServicelogger = new Mock<ILogger<CustomerTransactionService>>();
        var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();

        IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
        IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
        ICustomerTransactionService service = new CustomerTransactionService(_mockServicelogger.Object, _TransacRepo, _AccRepo);


        var account = new Accounts();
        account.AccountNumber = 11111;
        account.AccountType = "Savings";
        account.Balance = 2000;
        account.CustomerID = 1;
        account.IFSCCode = "SBI1";
        account.Status = "Approved";

        var account1 = await _AccRepo.GetByID(account.AccountNumber);
        Console.WriteLine(account1);
        account = await _AccRepo.Add(account1);
        Console.WriteLine(account);
        Assert.That(account.AccountNumber == 11111);
        //Assert.Pass();
    }
}
