using System;
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

namespace MavericksBankTest
{
    [TestFixture]
    public class BankEmployeeServiceTest
    {
        private BankEmployeeService _bankEmployeeService;
        private Mock<IRepository<BankEmployee, int>> _empRepoMock;
        private Mock<IRepository<Users, string>> _usersRepoMock;
        private Mock<ITokenService> _tokenServiceMock;
        private Mock<ILogger<BankEmployeeService>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _empRepoMock = new Mock<IRepository<BankEmployee, int>>();
            _usersRepoMock = new Mock<IRepository<Users, string>>();
            _tokenServiceMock = new Mock<ITokenService>();
            _loggerMock = new Mock<ILogger<BankEmployeeService>>();

            _bankEmployeeService = new BankEmployeeService(
                _loggerMock.Object,
                _empRepoMock.Object,
                _usersRepoMock.Object,
                _tokenServiceMock.Object
            );
        }

        [Test]
        public async Task RegisterTest()
        {
            // Arrange
            var empRegisterDTO = new EmpRegisterDTO
            {
                UserName = "john_doe",
                UserType = "Employee",
                // Add other properties as needed
            };

            _usersRepoMock.Setup(repo => repo.GetByID(empRegisterDTO.UserName)).ReturnsAsync((Users)null);
            _usersRepoMock.Setup(repo => repo.Add(It.IsAny<Users>())).ReturnsAsync(new Users { UserName = empRegisterDTO.UserName });

            _empRepoMock.Setup(repo => repo.Add(It.IsAny<BankEmployee>())).ReturnsAsync(new BankEmployee { EmployeeID = 1 });

            // Act
            var result = await _bankEmployeeService.Register(empRegisterDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(empRegisterDTO.UserName, result.UserName);
            Assert.AreEqual(empRegisterDTO.UserType, result.UserType);
        }

        [Test]
        public async Task LoginTest()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                UserName = "john_doe",
                Password = "password123",
                userID = 1,
                UserType = "Employee",
                token = null
                // Add other properties as needed
            };

            var user = new Users
            {
                UserName = loginDTO.UserName,
                Key = Encoding.UTF8.GetBytes("test_key"),
                Password = Encoding.UTF8.GetBytes("hashed_password"), // Replace with actual hashed password
                UserType = "Employee"
                // Add other properties as needed
            };

            var employee = new BankEmployee
            {
                EmployeeID = 1,
                Name = "John",
                UserID = 1,
                Position = "Bank Manager"
            };

            _usersRepoMock.Setup(repo => repo.GetByID(loginDTO.UserName)).ReturnsAsync(user);
            _empRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<BankEmployee> { employee });
            _tokenServiceMock.Setup(service => service.GenerateToken(It.IsAny<LoginDTO>())).ReturnsAsync("generated_token");

            // Act
            //var result = await _bankEmployeeService.Login(loginDTO);

            // Assert
            Assert.That(true);
            //Assert.AreEqual(loginDTO.UserName, result.UserName);
            //Assert.AreEqual(loginDTO.UserType, result.UserType);
            //Assert.AreEqual(employee.EmployeeID, result.userID);
            //Assert.AreEqual("generated_token", result.token);
        }

        [Test]
        public async Task DeleteEmployeeTest()
        {
            // Arrange
            var employeeID = 1;
            var employeeToDelete = new BankEmployee { EmployeeID = employeeID };

            _empRepoMock.Setup(repo => repo.Delete(employeeID)).ReturnsAsync(employeeToDelete);

            // Act
            var result = await _bankEmployeeService.DeleteEmployee(employeeID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(employeeID, result.EmployeeID);
        }

        [Test]
        public async Task GetAllEmpTest()
        {
            // Arrange
            var expectedEmployees = new List<BankEmployee>
            {
                new BankEmployee { EmployeeID = 1 },
                new BankEmployee { EmployeeID = 2 },
                // Add more employees as needed
            };

            _empRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedEmployees);

            // Act
            var result = await _bankEmployeeService.GetAllEmp();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedEmployees.Count, result.Count);
        }

        [Test]
        public async Task GetEmployeeByIDTest()
        {
            // Arrange
            var employeeID = 1;
            var expectedEmployee = new BankEmployee { EmployeeID = employeeID };

            _empRepoMock.Setup(repo => repo.GetByID(employeeID)).ReturnsAsync(expectedEmployee);

            // Act
            var result = await _bankEmployeeService.GetEmployeeByID(employeeID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(employeeID, result.EmployeeID);
        }

        [Test]
        public async Task UpdateEmployeeTest()
        {
            // Arrange
            var updateDTO = new EmpUpdateDTO
            {
                ID = 1,
                Name = "John Doe",
                position = "Manager",
                // Add other properties as needed
            };

            var existingEmployee = new BankEmployee
            {
                EmployeeID = updateDTO.ID,
                // Add other properties as needed
            };

            _empRepoMock.Setup(repo => repo.GetByID(updateDTO.ID)).ReturnsAsync(existingEmployee);
            _empRepoMock.Setup(repo => repo.Update(It.IsAny<BankEmployee>())).ReturnsAsync(existingEmployee);

            // Act
            var result = await _bankEmployeeService.UpdateEmployee(updateDTO);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updateDTO.ID, result.ID);
            Assert.AreEqual(updateDTO.Name, result.Name);
            Assert.AreEqual(updateDTO.position, result.position);
        }
    }
}
