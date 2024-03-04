using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Services;
using MavericksBank.Exceptions;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MavericksBank.Contexts;
using Microsoft.EntityFrameworkCore;
using MavericksBank.Repository;
using Microsoft.Extensions.Configuration;

namespace MavericksBankTest
{
    [TestFixture]
    public class CustomerServiceTest
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
        public async Task RegisterTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SecretKey"]).Returns("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiuru");

            // Act
            var tokenService = new TokenService(configurationMock.Object);

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            ICustomerAdminService service = new CustomerService(_mockServicelogger.Object,_CustRepo,_UserRepo,_TokenService);

            var user = new Users
            {
                UserID = 2,
                UserName = "SS"

            };

            var customerRegisterDTO = new CustomerRegisterDTO
            {
                UserName = "samsungmany",
                Password = "password123",
                Name = "Samson Joshua",
                Aadhaar = "12345",
                PANNumber = "12345",
                Gender = "Male",
                Age = 22,
                Phone = "12345",
                UserType = "Customer",
                Address = "Hyderabad",
                DOB = DateTime.Now,
            };



            // Act
            var result = await service.Register(customerRegisterDTO);

            Assert.IsNotNull(result);

        }


        [Test]
        [Order(2)]
        public async Task LoginTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SecretKey"]).Returns("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiurull");

            // Act
            var tokenService = new TokenService(configurationMock.Object);

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            ICustomerAdminService service = new CustomerService(_mockServicelogger.Object, _CustRepo, _UserRepo, _TokenService);

            var customerRegisterDTO = new CustomerRegisterDTO
            {
                UserName = "samsungman",
                Password = "password123",
                Name = "Samson Joshua",
                Aadhaar = "12345",
                PANNumber = "12345",
                Gender = "Male",
                Age = 22,
                Phone = "12345",
                UserType = "Customer",
                Address = "Hyderabad",
                DOB = DateTime.Now,
            };



            // Act
            var result2 = await service.Register(customerRegisterDTO);

            var loginDTO = new LoginDTO
            {
                UserName = "samsungman",
                Password = "password123",
            };

            byte[] GetEncryptedPassword(String password, byte[] key)
            {
                HMACSHA512 hmac = new HMACSHA512(key);
                byte[] userPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return userPassword;
            }

            var encryptedPwd = GetEncryptedPassword(loginDTO.Password, Encoding.UTF8.GetBytes("test_secret_key"));
            var user = new Users
            {
                UserName = loginDTO.UserName,
                Key = Encoding.UTF8.GetBytes("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiurull"),
                Password = encryptedPwd,
                UserType = "Customer"
            };

            var customer = new Customer
            {
                CustomerID = 1,
            };


            // Act
            var result = await service.Login(loginDTO);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        [Order(3)]
        public async Task GetAllCustomersTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SecretKey"]).Returns("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiurull");

            // Act
            var tokenService = new TokenService(configurationMock.Object);

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            ICustomerAdminService service = new CustomerService(_mockServicelogger.Object, _CustRepo, _UserRepo, _TokenService);

            var result = await service.GetAllCustomers();
            Assert.That(result.Count() == 2);
        }

        [Test]
        [Order(4)]
        public async Task GetCustomerByIDTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SecretKey"]).Returns("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiurull");

            // Act
            var tokenService = new TokenService(configurationMock.Object);

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            ICustomerAdminService service = new CustomerService(_mockServicelogger.Object, _CustRepo, _UserRepo, _TokenService);

            var result = await service.GetCustomerByID(1);
            Assert.IsNotNull(result);
        }


        [Test]
        [Order(5)]
        public async Task UpdateCustomerTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SecretKey"]).Returns("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiurull");

            // Act
            var tokenService = new TokenService(configurationMock.Object);

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            ICustomerAdminService service = new CustomerService(_mockServicelogger.Object, _CustRepo, _UserRepo, _TokenService);
            var custDTO = new CustomerNameDTO();
            custDTO.Address = "Bangalore";
            custDTO.ID = 1;
            custDTO.phoneNumber = "9011";
            custDTO.Name = "Samson Joshua";
            var result = await service.UpdateCustomerName(custDTO);
            Assert.IsNotNull(result);
        }

        [Test]
        [Order(6)]
        public async Task DeleteCustomerTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SecretKey"]).Returns("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiurull");

            // Act
            var tokenService = new TokenService(configurationMock.Object);

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            ICustomerAdminService service = new CustomerService(_mockServicelogger.Object, _CustRepo, _UserRepo, _TokenService);

            var customer = await service.DeleteCustomer(1);
            Assert.IsNotNull(customer);
        }




        [Test]
        [Order(5)]
        public async Task InvalidLoginTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SecretKey"]).Returns("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiurull");

            // Act
            var tokenService = new TokenService(configurationMock.Object);

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            ICustomerAdminService service = new CustomerService(_mockServicelogger.Object, _CustRepo, _UserRepo, _TokenService);

            var loginDTO = new LoginDTO
            {
                UserName = "samsungman",
                Password = "password1234",
            };

            byte[] GetEncryptedPassword(String password, byte[] key)
            {
                HMACSHA512 hmac = new HMACSHA512(key);
                byte[] userPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return userPassword;
            }

            var encryptedPwd = GetEncryptedPassword(loginDTO.Password, Encoding.UTF8.GetBytes("test_secret_key"));
            var user = new Users
            {
                UserName = loginDTO.UserName,
                Key = Encoding.UTF8.GetBytes("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiurull"),
                Password = encryptedPwd,
                UserType = "Customer"
            };

            var customer = new Customer
            {
                CustomerID = 1,
            };

            Assert.ThrowsAsync<InvalidUserException>(() => service.Login(loginDTO));

        }


        [Test]
        public async Task RegisterExistingCustomerTest()
        {
            var _mockServicelogger = new Mock<ILogger<CustomerService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c["SecretKey"]).Returns("test_secret_keynsmjxkfncifrncfirncifrjncfirncifjrncifrcnfrincifrcnfiurncfirncfiurncfrijncfuirncfirjncfijdcnfiuru");

            // Act
            var tokenService = new TokenService(configurationMock.Object);

            IRepository<Accounts, int> _AccRepo = new AccountsRepo(_mockAcclogger.Object, context);
            IRepository<Transactions, int> _TransacRepo = new TransactionsRepo(_mockTransaclogger.Object, context);
            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            IRepository<Banks, int> _BankRepo = new BanksRepo(_mockBanklogger.Object, context);
            IRepository<Branches, string> _BranchRepo = new BranchesRepo(_mockBranchlogger.Object, context);
            IRepository<Customer, int> _CustRepo = new CustomerRepo(_mockCustlogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            ICustomerAdminService service = new CustomerService(_mockServicelogger.Object, _CustRepo, _UserRepo, _TokenService);

            var user = new Users
            {
                UserID = 2,
                UserName = "SS"

            };

            var customerRegisterDTO = new CustomerRegisterDTO
            {
                UserName = "samsungmany",
                Password = "password123",
                Name = "Samson Joshua",
                Aadhaar = "12345",
                PANNumber = "12345",
                Gender = "Male",
                Age = 22,
                Phone = "12345",
                UserType = "Customer",
                Address = "Hyderabad",
                DOB = DateTime.Now,
            };



            Assert.ThrowsAsync<UserExistsException>(() => service.Register(customerRegisterDTO));
        }


    }
    }
