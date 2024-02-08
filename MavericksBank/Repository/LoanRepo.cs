using System;
using MavericksBank.Contexts;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Repository
{
	public class LoanRepo:IRepository<Loan,int>
	{
        private readonly ILogger<LoanRepo> _logger;
        private readonly RequestTrackerContext _context;
        public LoanRepo(ILogger<LoanRepo> logger, RequestTrackerContext context)
        {
            _logger = logger;
            _context = context;

        }

        public async Task<Loan> Add(Loan item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation($"Loan {item.LoanID} Added");
            return item;
        }

        public async Task<Loan> Delete(int item)
        {
            Loan loan = await GetByID(item);
            _context.Remove(loan);
            _context.SaveChanges();
            _logger.LogInformation($"Loan {loan.LoanID} Deleted");
            return loan;
        }

        public async Task<List<Loan>> GetAll()
        {
            _logger.LogInformation($"Loans retrieved");
            var loan = _context.Loans.ToList();
            if (loan != null)
                return loan;
            throw new NoLoanFoundException();
        }

        public async Task<Loan> GetByID(int key)
        {
            _logger.LogInformation($"Loan {key} retrieved");
            var loan = _context.Loans.SingleOrDefault(p => p.LoanID == key);
            if (loan != null)
                return loan;
            throw new NoLoanFoundException();
        }

        public async Task<Loan> Update(Loan item)
        {
            var loan = await GetByID(item.LoanID);
            _context.Entry<Loan>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation($"Loan {item.LoanID} Updated");
            return item;
        }
    }
}

