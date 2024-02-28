using System;
using MavericksBank.Contexts;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Repository;
using MavericksBank.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace MavericksBankTest
{
	public class CustomerBeneficiaryServiceTest
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
        public async Task AddBeneficiaryTest()
        {
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockServicelogger = new Mock<ILogger<CustomerBeneficiaryService>>();

            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            ICustomerBeneficiaryService service = new CustomerBeneficiaryService(_mockServicelogger.Object, _BenifRepo);


            var benif = new AddOrUpdateBenifDTO();
            benif.BeneFiciaryNumber = 22222;
            benif.BeneficiaryName = "Samson";
            benif.CustomerID = 1;
            benif.IFSCCode = "SBI1";

            benif = await service.AddBeneficiary(benif);
            Assert.That(benif.BeneFiciaryNumber == 22222);


        }

        [Test]
        [Order(2)]
        public async Task GetAllBeneficiaryTest()
        {
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockServicelogger = new Mock<ILogger<CustomerBeneficiaryService>>();

            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            ICustomerBeneficiaryService service = new CustomerBeneficiaryService(_mockServicelogger.Object, _BenifRepo);

            var benif = await service.GetAllBeneficiary(1);
            Assert.That(benif.Count()==2);


        }

        [Test]
        [Order(3)]
        public async Task GetBeneficiaryByIDTest()
        {
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockServicelogger = new Mock<ILogger<CustomerBeneficiaryService>>();

            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            ICustomerBeneficiaryService service = new CustomerBeneficiaryService(_mockServicelogger.Object, _BenifRepo);

            var benif = await service.GetBeneficiaryByID(22222);
            Assert.That(benif.BeneficiaryAccountNumber == 22222);


        }

        [Test]
        [Order(4)]
        public async Task UpdateBeneficiaryTest()
        {
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockServicelogger = new Mock<ILogger<CustomerBeneficiaryService>>();

            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            ICustomerBeneficiaryService service = new CustomerBeneficiaryService(_mockServicelogger.Object, _BenifRepo);

            var benifOld = await service.GetBeneficiaryByID(22222);
            benifOld.BeneficiaryName = "Samson Joshua";
            benifOld.IFSCCode = "SBI1";
            _BenifRepo.Update(benifOld);
            Assert.That(benifOld.BeneficiaryName=="Samson Joshua");


        }


        [Test]
        [Order(5)]
        public async Task DeleteBeneficiaryTest()
        {
            var _mockBeniflogger = new Mock<ILogger<BeneficiariesRepo>>();
            var _mockServicelogger = new Mock<ILogger<CustomerBeneficiaryService>>();

            IRepository<Beneficiaries, int> _BenifRepo = new BeneficiariesRepo(_mockBeniflogger.Object, context);
            ICustomerBeneficiaryService service = new CustomerBeneficiaryService(_mockServicelogger.Object, _BenifRepo);

            var benif = await service.DeleteBeneficiary(22222);
            var benifs = await service.GetAllBeneficiary(1);

            Assert.That(benifs.Count() == 1);


        }
    }
}

