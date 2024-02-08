using System;
using MavericksBank.Contexts;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Repository
{
	public class BanksRepo:IRepository<Banks,int>
	{
        private readonly ILogger<BanksRepo> _logger;
        private readonly RequestTrackerContext _context;
        public BanksRepo(ILogger<BanksRepo> logger, RequestTrackerContext context)
        {
            _logger = logger;
            _context = context;

        }

        public async Task<Banks> Add(Banks item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation($"Banks {item.BankID} Added");
            return item;
        }

        public async Task<Banks> Delete(int item)
        {
            Banks banks = await GetByID(item);
            _context.Remove(banks);
            _context.SaveChanges();
            _logger.LogInformation($"Bank {item} Deleted");
            return banks;
        }

        public async Task<List<Banks>> GetAll()
        {
            _logger.LogInformation($"Banks retrieved");
            var banks = _context.Banks.ToList();
            if (banks != null)
                return banks;
            throw new NoBankFoundException();
        }

        public async Task<Banks> GetByID(int key)
        {
            _logger.LogInformation($"Bank {key} retrieved");
            var banks = _context.Banks.SingleOrDefault(p => p.BankID == key);
            if (banks != null)
                return banks;
            throw new NoBankFoundException();
        }

        public async Task<Banks> Update(Banks item)
        {
            var banks = await GetByID(item.BankID);
            _context.Entry<Banks>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation($"Banks {item.BankID} Updated");
            return item;
        }
    }
}

