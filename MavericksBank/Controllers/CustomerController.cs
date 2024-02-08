using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Services;
using Microsoft.AspNetCore.Mvc;

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerAdminService _service;

        public CustomerController(ILogger<CustomerController> logger, ICustomerAdminService service)
        {
            _logger = logger;
            _service = service;

        }

        [Route("Register")]
        [HttpPost]
        public async Task<CustomerLoginDTO> Register(CustomerRegisterDTO customerRegister)
        {
            var customer = await _service.Register(customerRegister);
            _logger.LogInformation("Successfully Registered");
            return customer;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<CustomerLoginDTO> Login(CustomerLoginDTO customer)
        {
            customer = await _service.Login(customer);
            _logger.LogInformation("Successfully LoggedIn");
            return customer;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var customers = await _service.GetAllCustomers();
            _logger.LogInformation("All Customers Retrieved");
            return customers;
        }

        [Route("GetByID")]
        [HttpGet]
        public async Task<Customer> GetCustomerByIDAsync(int ID)
        {
            var customer = await _service.GetCustomerByID(ID);
            _logger.LogInformation($"Customer with ID {ID} Retrieved");
            return customer;
        }

        [Route("UpdateName")]
        [HttpPut]
        public async Task<CustomerNameDTO> UpdateCustomerNameAsync(CustomerNameDTO customer)
        {
            customer = await _service.UpdateCustomerName(customer);
            _logger.LogInformation("Customer Name Updated");
            return customer;
        }

        [Route("UpdatePhoneAndAddress")]
        [HttpPut]
        public async Task<CustomerPhoneAndAddressDTO> UpdateCustomerPhoneAndAddressAsync(CustomerPhoneAndAddressDTO customer)
        {
            customer = await _service.UpdateCustomerPhoneAndAddress(customer);
            _logger.LogInformation("Customer Phone and Address Updated");
            return customer;
        }

        [Route("Delete")]
        [HttpDelete]
        public async Task<Customer> DeleteCustomersAsync(int ID)
        {
            var customer = await _service.DeleteCustomer(ID);
            _logger.LogInformation("Customer Deleted");
            return customer;
        }

    }
}

