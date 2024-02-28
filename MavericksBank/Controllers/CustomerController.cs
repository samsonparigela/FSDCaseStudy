using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using MavericksBank.Models.DTO;
using MavericksBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MavericksBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MavericksBankPolicy")]
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
        public async Task<ActionResult<LoginDTO>> Register(CustomerRegisterDTO customerRegister)
        {
            try
            {
                var customer = await _service.Register(customerRegister);
                _logger.LogInformation("Successfully Registered");
                return customer;
            }
            catch (UserExistsException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<LoginDTO>> Login(LoginDTO customer)
        {
            try
            {
                customer = await _service.Login(customer);
                _logger.LogInformation("Successfully LoggedIn");
                return customer;
            }
            catch (InvalidUserException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomersAsync()
        {
            try
            {
                var customers = await _service.GetAllCustomers();
                _logger.LogInformation("All Customers Retrieved");
                return customers;
            }
            catch (NoCustomerFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Route("GetByID")]
        [HttpGet]
        public async Task<ActionResult<Customer>> GetCustomerByIDAsync(int ID)
        {
            try
            {
                var customer = await _service.GetCustomerByID(ID);
                _logger.LogInformation($"Customer with ID {ID} Retrieved");
                return customer;
            }
            catch (NoCustomerFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Customer")]
        [Route("UpdateCustomer")]
        [HttpPut]
        public async Task<ActionResult<CustomerNameDTO>> UpdateCustomerNameAsync(CustomerNameDTO customer)
        {
            try
            {
                customer = await _service.UpdateCustomerName(customer);
                _logger.LogInformation("Customer Updated");
                return customer;
            }
            catch (NoCustomerFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

        //[Authorize(Roles = "Customer")]
        //[Route("UpdatePhoneAndAddress")]
        //[HttpPut]
        //public async Task<ActionResult<CustomerPhoneAndAddressDTO>> UpdateCustomerPhoneAndAddressAsync(CustomerPhoneAndAddressDTO customer)
        //{
        //    try
        //    {
        //        customer = await _service.UpdateCustomerPhoneAndAddress(customer);
        //        _logger.LogInformation("Customer Phone and Address Updated");
        //        return customer;
        //    }
        //    catch (NoCustomerFoundException ex)
        //    {
        //        _logger.LogCritical(ex.Message);
        //        return NotFound(ex.Message);
        //    }
        //}

        [Route("Delete")]
        [HttpDelete]
        public async Task<ActionResult<Customer>> DeleteCustomersAsync(int ID)
        {
            try
            {
                var customer = await _service.DeleteCustomer(ID);
                _logger.LogInformation("Customer Deleted");
                return customer;
            }
            catch(NoCustomerFoundException ex)
            {
                _logger.LogCritical(ex.Message);
                return NotFound(ex.Message);
            }
        }

    }
}

