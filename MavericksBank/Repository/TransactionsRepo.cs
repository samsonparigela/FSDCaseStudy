using System;
using MavericksBank.Contexts;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Repository
{
	public class TransactionsRepo:IRepository<Transactions,int>
	{
        private readonly ILogger<TransactionsRepo> _logger;
        private readonly RequestTrackerContext _context;
        public TransactionsRepo(ILogger<TransactionsRepo> logger, RequestTrackerContext context)
        {
            _logger = logger;
            _context = context;

        }

        public async Task<Transactions> Add(Transactions item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation($"Transaction {item.TransactionID} Added");
            return item;
        }

        public async Task<Transactions> Delete(int item)
        {
            Transactions transaction = await GetByID(item);
            _context.Remove(transaction);
            _context.SaveChanges();
            _logger.LogInformation($"Transaction {transaction.TransactionID} Deleted");
            return transaction;
        }

        public async Task<List<Transactions>> GetAll()
        {
            _logger.LogInformation($"Transactions retrieved");
            var transactions = _context.Transactions.ToList();
            if (transactions != null)
                return transactions;
            throw new NoTransactionsFoundException();
        }

        public async Task<Transactions> GetByID(int key)
        {
            _logger.LogInformation($"Transaction {key} retrieved");
            var transaction = _context.Transactions.SingleOrDefault(p => p.TransactionID == key);
            if (transaction != null)
                return transaction;
            throw new NoTransactionsFoundException();
        }

        public async Task<Transactions> Update(Transactions item)
        {
            var transaction = await GetByID(item.TransactionID);
            _context.Entry<Transactions>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation($"Transaction {item.TransactionID} Updated");
            return item;
        }
    }
}

