using System;
using MavericksBank.Contexts;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MavericksBank.Repository
{
	public class LoanPoliciesRepo:IRepository<LoanPolicies,int>
	{
        private readonly ILogger<LoanPoliciesRepo> _logger;
        private readonly RequestTrackerContext _context;
        public LoanPoliciesRepo(ILogger<LoanPoliciesRepo> logger, RequestTrackerContext context)
        {
            _logger = logger;
            _context = context;

        }

        public async Task<LoanPolicies> Add(LoanPolicies item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation($"Loan Policies Added");
            return item;
        }

        public async Task<LoanPolicies> Delete(int item)
        {
            LoanPolicies loanPolicy = await GetByID(item);
            _context.Remove(loanPolicy);
            _context.SaveChanges();
            _logger.LogInformation($"Loan Policy {loanPolicy.LoanPolicyID} Deleted");
            return loanPolicy;
        }

        public async Task<List<LoanPolicies>> GetAll()
        {
            _logger.LogInformation($"Loan Policies retrieved");
            var loanPolicies = _context.LoanPolicies.ToList();
            if (loanPolicies != null)
                return loanPolicies;
            throw new NoLoanFoundException("No Loan policies exist");
        }

        public async Task<LoanPolicies> GetByID(int key)
        {
            _logger.LogInformation($"Loan Policy {key} retrieved");
            var loanPolicy = _context.LoanPolicies.SingleOrDefault(p => p.LoanPolicyID == key);
            if (loanPolicy != null)
                return loanPolicy;
            throw new NoLoanFoundException($"No loanpolicy exist with ID {key}");
        }

        public async Task<LoanPolicies> Update(LoanPolicies item)
        {
            var loan = await GetByID(item.LoanPolicyID);
            if (loan == null)
                throw new NoLoanFoundException($"Cannot update. Loan {item.LoanPolicyID} doesnot exist");
            _context.Entry<LoanPolicies>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation($"Loan Policy {item.LoanPolicyID} Updated");
            return item;
        }
    }
}

