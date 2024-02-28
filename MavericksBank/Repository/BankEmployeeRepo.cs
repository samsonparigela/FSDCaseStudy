using System;
using MavericksBank.Contexts;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Repository
{
	public class BankEmployeeRepo:IRepository<BankEmployee,int>
	{
        private readonly ILogger<BankEmployeeRepo> _logger;
        private readonly RequestTrackerContext _context;
        public BankEmployeeRepo(ILogger<BankEmployeeRepo> logger, RequestTrackerContext context)
        {
            _logger = logger;
            _context = context;

        }

        public async Task<BankEmployee> Add(BankEmployee item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation($"BankEmployee {item.EmployeeID} Added");
            return item;
        }

        public async Task<BankEmployee> Delete(int item)
        {
            BankEmployee bankEmployee = await GetByID(item);
            if (bankEmployee == null)
                throw new NoBankEmployeeFoundException();
            _context.Remove(bankEmployee);
            _context.SaveChanges();
            _logger.LogInformation($"BankEmployee {item} Deleted");
            return bankEmployee;
        }

        public async Task<List<BankEmployee>> GetAll()
        {
            _logger.LogInformation($"BankEmployees retrieved");
            var bankEmployees = _context.BankEmployee.ToList();
            if (bankEmployees != null)
                return bankEmployees;
            throw new NoBankEmployeeFoundException();
        }

        public async Task<BankEmployee> GetByID(int key)
        {
            _logger.LogInformation($"BankEmployee {key} retrieved");
            var bankEmployee = _context.BankEmployee.SingleOrDefault(p => p.EmployeeID == key);
            if (bankEmployee != null)
                return bankEmployee;
            throw new NoBankEmployeeFoundException();
        }

        public async Task<BankEmployee> Update(BankEmployee item)
        {
            var bankEmployee = await GetByID(item.EmployeeID);
            if (bankEmployee == null)
                throw new NoBankEmployeeFoundException();
            _context.Entry<BankEmployee>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation($"BankEmployee {item.EmployeeID} Updated");
            return item;
        }
    }
}

