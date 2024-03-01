using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace MavericksBankTest
{
    [TestFixture]
    public class BranchServiceTest
    {
        private BranchService _branchService;
        private Mock<IRepository<Branches, string>> _branchRepoMock;
        private Mock<ILogger<BranchService>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _branchRepoMock = new Mock<IRepository<Branches, string>>();
            _loggerMock = new Mock<ILogger<BranchService>>();

            _branchService = new BranchService(
                _loggerMock.Object,
                _branchRepoMock.Object
            );
        }

        [Test]
        public async Task AddBranchTest()
        {
            // Arrange
            var branchCreateDTO = new BranchCreateDTO
            {
                BankID = 1,
                BranchName = "Main Branch",
                IFSCCode = "ICICI123",
            };

            // Act
            var addedBranch = await _branchService.AddBranch(branchCreateDTO);

            // Assert
            Assert.IsNotNull(addedBranch);
            Assert.AreEqual(branchCreateDTO.BranchName, addedBranch.BranchName);
        }

        [Test]
        public async Task DeleteBranchTest()
        {
            // Arrange
            var branchToDelete = new Branches
            {
                IFSCCode = "ICICI123",
                BranchName = "Main Branch",
                BankID = 1
            };

            _branchRepoMock.Setup(repo => repo.GetByID(branchToDelete.IFSCCode)).ReturnsAsync(branchToDelete);
            _branchRepoMock.Setup(repo => repo.Delete(branchToDelete.IFSCCode)).ReturnsAsync(branchToDelete);

            // Act
            var deletedBranch = await _branchService.DeleteBranch(branchToDelete.IFSCCode);

            // Assert
            Assert.IsNotNull(deletedBranch);
            Assert.AreEqual(branchToDelete.IFSCCode, deletedBranch.IFSCCode);
        }

        [Test]
        public async Task GetAllBranchesTest()
        {
            // Arrange
            var expectedBranches = new List<Branches>
            {
                new Branches { IFSCCode = "ICICI123", BranchName = "Main Branch", BankID = 1 },
                new Branches { IFSCCode = "HDFC123", BranchName = "Downtown Branch", BankID = 1 }
                // Add more branches as needed
            };

            _branchRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedBranches);

            // Act
            var result = await _branchService.GetAllBranches();

            // Assert
            Assert.AreEqual(expectedBranches.Count, result.Count);
        }

        [Test]
        public async Task GetBranchByIDTest()
        {
            // Arrange
            var branch = new Branches
            {
                IFSCCode = "ICICI123",
                BranchName = "Main Branch",
                BankID = 1
            };

            _branchRepoMock.Setup(repo => repo.GetByID(branch.IFSCCode)).ReturnsAsync(branch);

            // Act
            var fetchedBranch = await _branchService.GetBranchbyID(branch.IFSCCode);

            // Assert
            Assert.IsNotNull(fetchedBranch);
            Assert.AreEqual(branch.IFSCCode, fetchedBranch.IFSCCode);
        }

        [Test]
        public async Task UpdateBranchTest()
        {
            // Arrange
            var branchToUpdate = new Branches
            {
                IFSCCode = "ICICI123",
                BranchName = "Main Branch",
                BankID = 1
            };

            var updatedBranchInfo = new BranchUpdateDTO
            {
                IFSCCode = branchToUpdate.IFSCCode,
                BranchName = "Updated Main Branch"
                // Add other properties as needed
            };

            _branchRepoMock.Setup(repo => repo.GetByID(branchToUpdate.IFSCCode)).ReturnsAsync(branchToUpdate);
            _branchRepoMock.Setup(repo => repo.Update(It.IsAny<Branches>())).ReturnsAsync(branchToUpdate);

            // Act
            var updatedBranch = await _branchService.UpdateBranch(updatedBranchInfo);

            // Assert
            Assert.IsNotNull(updatedBranch);
            Assert.AreEqual(updatedBranchInfo.IFSCCode, updatedBranch.IFSCCode);
            Assert.AreEqual(updatedBranchInfo.BranchName, updatedBranch.BranchName);
            // Add additional assertions for other properties if needed
        }

        // Add more test methods as needed
    }
}
