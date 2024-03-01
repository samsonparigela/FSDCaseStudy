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

namespace MavericksBankTest
{
    [TestFixture]
    public class CustomerServiceTest
    {
        private CustomerService _customerService;
        private Mock<IRepository<Customer, int>> _customerRepoMock;
        private Mock<IRepository<Users, string>> _userRepoMock;
        private Mock<ITokenService> _tokenServiceMock;
        private Mock<ILogger<CustomerService>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _customerRepoMock = new Mock<IRepository<Customer, int>>();
            _userRepoMock = new Mock<IRepository<Users, string>>();
            _tokenServiceMock = new Mock<ITokenService>();
            _loggerMock = new Mock<ILogger<CustomerService>>();

            _customerService = new CustomerService(
                _loggerMock.Object,
                _customerRepoMock.Object,
                _userRepoMock.Object,
                _tokenServiceMock.Object
            );
        }

        [Test]
        public async Task RegisterTest()
        {
            // Arrange
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
                Address = "Hyderabad"
            };

            var user = new Users
            {
                UserID = 1,
                UserName = customerRegisterDTO.UserName,
            };

            var customer = new Customer
            {
                CustomerID = 1,
                Name = "Samson Joshua",
                Aadhaar = "12345",
                PANNumber = "12345",
                Gender = "Male",
                Age = 22,
                Phone = "12345",
                Address = "Hyderabad",
                UserID = 1
            };

            _userRepoMock.Setup(repo => repo.GetByID(customerRegisterDTO.UserName)).ReturnsAsync((Users)null);
            _userRepoMock.Setup(repo => repo.Add(It.IsAny<Users>())).ReturnsAsync(user);
            _customerRepoMock.Setup(repo => repo.Add(It.IsAny<Customer>())).ReturnsAsync(customer);

            // Act
            var result = await _customerService.Register(customerRegisterDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(customerRegisterDTO.UserName, result.UserName);
            Assert.AreEqual("Customer", result.UserType);
            // Add other assertions as needed
        }

        [Test]
        public async Task RegisterExistingUSer()
        {
            // Arrange
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
                Address = "Hyderabad"
            };

            var existingUser = new Users
            {
                UserName = customerRegisterDTO.UserName
            };

            _userRepoMock.Setup(repo => repo.GetByID(customerRegisterDTO.UserName)).ReturnsAsync(existingUser);

            Assert.ThrowsAsync<UserExistsException>(() => _customerService.Register(customerRegisterDTO));
        }

        [Test]
        public async Task LoginTest()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                UserName = "samsungman",
                Password = "password123",
                // Add other properties as needed
            };

            byte[] GetEncryptedPassword(String password, byte[] key)
            {
                HMACSHA512 hmac = new HMACSHA512(key);
                byte[] userPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return userPassword;
            }

            var encryptedPwd = GetEncryptedPassword(loginDTO.Password, Encoding.UTF8.GetBytes("test_key"));
            var user = new Users
            {
                UserName = loginDTO.UserName,
                Key = Encoding.UTF8.GetBytes("test_key"),
                Password = encryptedPwd,
                UserType = "Customer"
            };

            var customer = new Customer
            {
                CustomerID = 1,
            };

            _userRepoMock.Setup(repo => repo.GetByID(loginDTO.UserName)).ReturnsAsync(user);
            _customerRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Customer> { customer });
            _tokenServiceMock.Setup(service => service.GenerateToken(It.IsAny<LoginDTO>())).ReturnsAsync("generated_token");

            // Act
            var result = await _customerService.Login(loginDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(loginDTO.UserName, result.UserName);
            Assert.AreEqual("Customer", result.UserType);
            Assert.AreEqual(customer.CustomerID, result.userID);
            Assert.AreEqual("generated_token", result.token);
        }

        [Test]
        public async Task LoginInvalidTest()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                UserName = "john_doe",
                Password = "password123",
                userID = 1,
                UserType = "Customer"
            };

            byte[] GetEncryptedPassword(String password, byte[] key)
            {
                HMACSHA512 hmac = new HMACSHA512(key);
                byte[] userPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return userPassword;
            }
            var encryptedPwd = GetEncryptedPassword(loginDTO.Password+"2", Encoding.UTF8.GetBytes("test_key"));
            var user = new Users
            {
                UserName = loginDTO.UserName,
                Key = Encoding.UTF8.GetBytes("test_key"),
                Password = encryptedPwd,
                UserType = "Customer"
            };

            var customer = new Customer
            {
                CustomerID = 1,
                Name = "Samson Joshua",
                Aadhaar = "12345",
                PANNumber = "12345",
                Gender = "Male",
                Age = 22,
                Phone = "12345",
                Address = "Hyderabad"
            };

            _userRepoMock.Setup(repo => repo.GetByID(loginDTO.UserName)).ReturnsAsync(user);
            _customerRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Customer> { customer });
            _tokenServiceMock.Setup(service => service.GenerateToken(It.IsAny<LoginDTO>())).ReturnsAsync("generated_token");

            Assert.ThrowsAsync<InvalidUserException>(() => _customerService.Login(loginDTO));
        }

        [Test]
        public async Task RegisterNewUserTest()
        {
            // Arrange
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
                Address = "Hyderabad"
            };

            var user = new Users
            {
                UserName = customerRegisterDTO.UserName,
            };

            var customer = new Customer
            {
                Name = "Samson Joshua",
                Aadhaar = "12345",
                PANNumber = "12345",
                Gender = "Male",
                Age = 22,
                Phone = "12345",
                Address = "Hyderabad"
            };

            _userRepoMock.Setup(repo => repo.GetByID(customerRegisterDTO.UserName)).ReturnsAsync((Users)null);
            _userRepoMock.Setup(repo => repo.Add(It.IsAny<Users>())).ReturnsAsync(user);
            _customerRepoMock.Setup(repo => repo.Add(It.IsAny<Customer>())).ReturnsAsync(customer);

            // Act
            var result = await _customerService.Register(customerRegisterDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(customerRegisterDTO.UserName, result.UserName);
            Assert.AreEqual("Customer", result.UserType);
        }

        [Test]
        public async Task RegisterExistingCustomerTest()
        {
            // Arrange
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
                Address = "Hyderabad"
            };

            var existingUser = new Users
            {
                UserName = customerRegisterDTO.UserName,
            };

            _userRepoMock.Setup(repo => repo.GetByID(customerRegisterDTO.UserName)).ReturnsAsync(existingUser);

            Assert.ThrowsAsync<UserExistsException>(() => _customerService.Register(customerRegisterDTO));
        }

        [Test]
        public async Task DeleteCustomerTest()
        {
            // Arrange
            int customerIdToDelete = 1;
            var deletedCustomer = new Customer { CustomerID = 1, Name = "Samson", Phone = "9014445354", Aadhaar = "12345", Address = "Hyd", Age = 21, Gender = "Male", PANNumber = "12345", UserID = 1 };

            _customerRepoMock.Setup(repo => repo.GetByID(customerIdToDelete)).ReturnsAsync(deletedCustomer);
            _customerRepoMock.Setup(repo => repo.Delete(customerIdToDelete)).ReturnsAsync(deletedCustomer);

            // Act
            var result = await _customerService.DeleteCustomer(customerIdToDelete);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(customerIdToDelete, result.CustomerID);
        }

        [Test]
        public async Task GetCustomerByIDTest()
        {
            // Arrange
            int customerIdToRetrieve = 1;
            var expectedCustomer = new Customer { CustomerID = 1, Name = "Samson", Phone = "9014445354",Aadhaar="12345",Address="Hyd",Age=21,Gender="Male",PANNumber="12345",UserID=1 };

            _customerRepoMock.Setup(repo => repo.GetByID(customerIdToRetrieve)).ReturnsAsync(expectedCustomer);

            // Act
            var result = await _customerService.GetCustomerByID(customerIdToRetrieve);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(customerIdToRetrieve, result.CustomerID);
        }

        [Test]
        public async Task GetAllCustomersTest()
        {
            // Arrange
            var expectedCustomers = new List<Customer>
            {
                new Customer { CustomerID = 1, Name = "Samson", Phone = "9014445354",Aadhaar="12345",Address="Hyd",Age=21,Gender="Male",PANNumber="12345",UserID=1 },
                new Customer { CustomerID = 2, Name = "Joshua", Phone = "9014445354",Aadhaar="12345",Address="Hyd",Age=21,Gender="Male",PANNumber="12345",UserID=1 }
            };

            _customerRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedCustomers);

            // Act
            var result = await _customerService.GetAllCustomers();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCustomers.Count, result.Count);
        }

        [Test]
        public async Task UpdateCustomerTest()
        {
            // Arrange
            var customerToUpdate = new CustomerNameDTO
            {
                ID = 1,
                Name = "John Doe",
                phoneNumber = "1234567890",
                Address = "123 Main St"
            };

            var updatedCustomer = new Customer
            {
                CustomerID = customerToUpdate.ID,
                Name = customerToUpdate.Name,
                Phone = customerToUpdate.phoneNumber,
                Address = customerToUpdate.Address
            };

            _customerRepoMock.Setup(repo => repo.GetByID(customerToUpdate.ID)).ReturnsAsync(updatedCustomer);
            _customerRepoMock.Setup(repo => repo.Update(It.IsAny<Customer>())).ReturnsAsync(updatedCustomer);

            // Act
            var result = await _customerService.UpdateCustomerName(customerToUpdate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(customerToUpdate.ID, result.ID);
            Assert.AreEqual(customerToUpdate.Name, result.Name);
            Assert.AreEqual(customerToUpdate.phoneNumber, result.phoneNumber);
            Assert.AreEqual(customerToUpdate.Address, result.Address);
        }

    }
}
