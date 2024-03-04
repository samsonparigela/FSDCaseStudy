﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Exceptions;
using MavericksBank.Services;
using System.Text;
using MavericksBank.Contexts;
using Microsoft.EntityFrameworkCore;
using MavericksBank.Repository;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace MavericksBankTest
{
    [TestFixture]
    public class BankEmployeeServiceTest
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
            var _mockServicelogger = new Mock<ILogger<BankEmployeeService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();
            var _mockBankEmplogger = new Mock<ILogger<BankEmployeeRepo>>();

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
            IRepository<BankEmployee, int> _BankEmpRepo = new BankEmployeeRepo(_mockBankEmplogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            IEmployeeAdminService service = new BankEmployeeService(_mockServicelogger.Object, _BankEmpRepo, _UserRepo, _TokenService);

            var user = new Users
            {
                UserID = 2,
                UserName = "SS"

            };

            var empRegisterDTO = new EmpRegisterDTO
            {
                UserName = "samsungmanny",
                Password = "password1234",
                Name = "Samson Joshua",
                Position="Bank Manager"
            };



            // Act
            var result = await service.Register(empRegisterDTO);

            Assert.IsNotNull(result);
        }

        [Test]
        [Order(2)]
        public async Task LoginTest()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmployeeService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();
            var _mockBankEmplogger = new Mock<ILogger<BankEmployeeRepo>>();

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
            IRepository<BankEmployee, int> _BankEmpRepo = new BankEmployeeRepo(_mockBankEmplogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            IEmployeeAdminService service = new BankEmployeeService(_mockServicelogger.Object, _BankEmpRepo, _UserRepo, _TokenService);

            var empRegisterDTO = new EmpRegisterDTO
            {
                UserName = "samsungman2",
                Password = "password1234",
                Name = "Samson Joshua",
                Position = "Bank Manager"
            };
            var result2 = await service.Register(empRegisterDTO);

            var loginDTO = new LoginDTO
            {
                UserName = "samsungman2",
                Password = "password1234",
            };


            // Act
            var result = await service.Login(loginDTO);

            Assert.IsNotNull(result);
        }

        [Test]
        [Order(3)]
        public async Task GetAllEmployees()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmployeeService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();
            var _mockBankEmplogger = new Mock<ILogger<BankEmployeeRepo>>();

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
            IRepository<BankEmployee, int> _BankEmpRepo = new BankEmployeeRepo(_mockBankEmplogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            IEmployeeAdminService service = new BankEmployeeService(_mockServicelogger.Object, _BankEmpRepo, _UserRepo, _TokenService);


            var result = await service.GetAllEmp();

            Assert.That(result.Count()==2);

        }

        [Test]
        [Order(4)]
        public async Task GetEmployeesByID()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmployeeService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();
            var _mockBankEmplogger = new Mock<ILogger<BankEmployeeRepo>>();

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
            IRepository<BankEmployee, int> _BankEmpRepo = new BankEmployeeRepo(_mockBankEmplogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            IEmployeeAdminService service = new BankEmployeeService(_mockServicelogger.Object, _BankEmpRepo, _UserRepo, _TokenService);


            var result = await service.GetEmployeeByID(1);

            Assert.IsNotNull(result);

        }

        [Test]
        [Order(5)]
        public async Task UpdateEmp()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmployeeService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();
            var _mockBankEmplogger = new Mock<ILogger<BankEmployeeRepo>>();

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
            IRepository<BankEmployee, int> _BankEmpRepo = new BankEmployeeRepo(_mockBankEmplogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            IEmployeeAdminService service = new BankEmployeeService(_mockServicelogger.Object, _BankEmpRepo, _UserRepo, _TokenService);

            var empDTO = new EmpUpdateDTO();
            empDTO.ID = 1;
            empDTO.Name = "Joshua";
            empDTO.position = "Senior Bank Manager";
            var result = await service.UpdateEmployee(empDTO);

            Assert.That(result.Name=="Joshua");

        }


        [Test]
        [Order(6)]
        public async Task DeleteEmployee()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmployeeService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();
            var _mockBankEmplogger = new Mock<ILogger<BankEmployeeRepo>>();

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
            IRepository<BankEmployee, int> _BankEmpRepo = new BankEmployeeRepo(_mockBankEmplogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            IEmployeeAdminService service = new BankEmployeeService(_mockServicelogger.Object, _BankEmpRepo, _UserRepo, _TokenService);


            var result = await service.DeleteEmployee(1);

            Assert.IsNotNull(result);

        }

        [Test]
        [Order(7)]
        public async Task InvalidLoginTest()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmployeeService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();
            var _mockBankEmplogger = new Mock<ILogger<BankEmployeeRepo>>();

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
            IRepository<BankEmployee, int> _BankEmpRepo = new BankEmployeeRepo(_mockBankEmplogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            IEmployeeAdminService service = new BankEmployeeService(_mockServicelogger.Object, _BankEmpRepo, _UserRepo, _TokenService);


            var empRegisterDTO = new EmpRegisterDTO
            {
                UserName = "samsungman26",
                Password = "password1234",
                Name = "Samson Joshua",
                Position = "Bank Manager"
            };
            var result2 = await service.Register(empRegisterDTO);

            var loginDTO = new LoginDTO
            {
                UserName = "samsungman26",
                Password = "password12345",
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
                UserType = "Bank Employee"
            };


            // Act

            Assert.ThrowsAsync<InvalidUserException>(() => service.Login(loginDTO));
        }

        [Test]
        [Order(8)]
        public async Task RegisterExistingUserTest()
        {
            var _mockServicelogger = new Mock<ILogger<BankEmployeeService>>();
            var _mockAcclogger = new Mock<ILogger<AccountsRepo>>();
            var _mockTransaclogger = new Mock<ILogger<TransactionsRepo>>();
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockBanklogger = new Mock<ILogger<BanksRepo>>();
            var _mockBranchlogger = new Mock<ILogger<BranchesRepo>>();
            var _mockCustlogger = new Mock<ILogger<CustomerRepo>>();
            var _mockUserlogger = new Mock<ILogger<UsersRepo>>();
            var _mockBankEmplogger = new Mock<ILogger<BankEmployeeRepo>>();

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
            IRepository<BankEmployee, int> _BankEmpRepo = new BankEmployeeRepo(_mockBankEmplogger.Object, context);
            IRepository<Users, string> _UserRepo = new UsersRepo(_mockUserlogger.Object, context);

            ITokenService _TokenService = new TokenService(configurationMock.Object);
            IEmployeeAdminService service = new BankEmployeeService(_mockServicelogger.Object, _BankEmpRepo, _UserRepo, _TokenService);

            var user = new Users
            {
                UserID = 2,
                UserName = "SS"

            };

            var empRegisterDTO = new EmpRegisterDTO
            {
                UserName = "samsungmanny",
                Password = "password1234",
                Name = "Samson Joshua",
                Position = "Bank Manager"
            };



            // Act
            Assert.ThrowsAsync<UserExistsException>(() => service.Register(empRegisterDTO));

        }

    }
}
