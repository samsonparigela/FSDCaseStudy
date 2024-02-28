using System;
using System.Numerics;
using MavericksBank.Contexts;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Repository
{
	public class CustomerRepo:IRepository<Customer,int>
	{
        private readonly ILogger<CustomerRepo> _logger;
        private readonly RequestTrackerContext _context;
        public CustomerRepo(ILogger<CustomerRepo> logger,RequestTrackerContext context)
		{
            _logger = logger;
            _context = context;

		}

        public async Task<Customer> Add(Customer item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation($"Customer {item.CustomerID} Added");
            return item;
        }

        public async Task<Customer> Delete(int item)
        {
            Customer customer = await GetByID(item);
            if (customer == null)
                throw new NoCustomerFoundException();
            _context.Remove(customer);
            _context.SaveChanges();
            _logger.LogInformation($"Customer {customer.CustomerID} Deleted");
            return customer;
        }

        public async Task<List<Customer>> GetAll()
        {
            _logger.LogInformation($"Customers retrieved");
            var customers = _context.Customers.ToList();
            if (customers != null)
                return customers;
            throw new NoCustomerFoundException();
        }

        public async Task<Customer> GetByID(int key)
        {
            _logger.LogInformation($"Customer {key} retrieved");
            var customer=_context.Customers.SingleOrDefault(p => p.CustomerID == key);
            if (customer != null)
                return customer;
            throw new NoCustomerFoundException();
        }

        public async Task<Customer> Update(Customer item)
        {
            var customer = await GetByID(item.CustomerID);
            if (customer == null)
                throw new NoCustomerFoundException();
            _context.Entry<Customer>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation($"Customer {item.CustomerID} Updated");
            return item;
        }
    }
}



