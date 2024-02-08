using System;
using MavericksBank.Contexts;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace MavericksBank.Repository
{
	public class AccountsRepo:IRepository<Accounts,int>
	{
        private readonly ILogger<AccountsRepo> _logger;
        private readonly RequestTrackerContext _context;
        public AccountsRepo(ILogger<AccountsRepo> logger, RequestTrackerContext context)
        {
            _logger = logger;
            _context = context;

        }

        public async Task<Accounts> Add(Accounts item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation($"Accounts {item.AccountNumber} Added");
            return item;
        }

        public async Task<Accounts> Delete(int item)
        {
            Accounts accounts = await GetByID(item);
            _context.Remove(accounts);
            _context.SaveChanges();
            _logger.LogInformation($"Account {accounts.AccountNumber} Deleted");
            return accounts;
        }

        public async Task<List<Accounts>> GetAll()
        {
            _logger.LogInformation($"Accounts retrieved");
            var accounts = _context.Accounts.ToList();
            if (accounts != null)
                return accounts;
            throw new NoAccountFoundException();
        }

        public async Task<Accounts> GetByID(int key)
        {
            _logger.LogInformation($"Accounts {key} retrieved");
            var accounts = _context.Accounts.SingleOrDefault(p => p.AccountID == key);
            if (accounts != null)
                return accounts;
            throw new NoAccountFoundException();
        }

        public async Task<Accounts> Update(Accounts item)
        {
            var accounts = await GetByID(item.AccountID);
            _context.Entry<Accounts>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation($"Accounts {item.AccountNumber} Updated");
            return item;
        }
    }
}

