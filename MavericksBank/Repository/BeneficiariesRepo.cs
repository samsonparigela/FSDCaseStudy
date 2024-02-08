using System;
using MavericksBank.Contexts;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Repository
{
	public class BeneficiariesRepo:IRepository<Beneficiaries,int>
	{
        private readonly ILogger<BeneficiariesRepo> _logger;
        private readonly RequestTrackerContext _context;
        public BeneficiariesRepo(ILogger<BeneficiariesRepo> logger, RequestTrackerContext context)
        {
            _logger = logger;
            _context = context;

        }

        public async Task<Beneficiaries> Add(Beneficiaries item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation($"Beneficiary {item.BeneficiaryAccountNumber} Added");
            return item;
        }

        public async Task<Beneficiaries> Delete(int item)
        {
            Beneficiaries beneficiary = await GetByID(item);
            _context.Remove(beneficiary);
            _context.SaveChanges();
            _logger.LogInformation($"Beneficiary {item} Deleted");
            return beneficiary;
        }

        public async Task<List<Beneficiaries>> GetAll()
        {
            _logger.LogInformation($"Beneficiaries retrieved");
            var beneficiaries = _context.Beneficiaries.ToList();
            if (beneficiaries != null)
                return beneficiaries;
            throw new NoBeneficiariesFoundException();
        }

        public async Task<Beneficiaries> GetByID(int key)
        {
            _logger.LogInformation($"Beneficiary {key} retrieved");
            var beneficiaries = _context.Beneficiaries.SingleOrDefault(p => p.BeneficiaryAccountNumber == key);
            if (beneficiaries != null)
                return beneficiaries;
            throw new NoBeneficiariesFoundException();
        }

        public async Task<Beneficiaries> Update(Beneficiaries item)
        {
            var beneficiaries = await GetByID(item.BeneficiaryAccountNumber);
            _context.Entry<Beneficiaries>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation($"Beneficiary {item} Updated");
            return item;
        }
    }
}

