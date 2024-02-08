using System;
using MavericksBank.Contexts;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Repository
{
	public class BranchesRepo:IRepository<Branches,string>
	{
        private readonly ILogger<BranchesRepo> _logger;
        private readonly RequestTrackerContext _context;
        public BranchesRepo(ILogger<BranchesRepo> logger, RequestTrackerContext context)
        {
            _logger = logger;
            _context = context;

        }

        public async Task<Branches> Add(Branches item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation($"Branch {item.BranchName} Added");
            return item;
        }

        public async Task<Branches> Delete(string item)
        {
            Branches branches = await GetByID(item);
            _context.Remove(branches);
            _context.SaveChanges();
            _logger.LogInformation($"Branch {branches.BranchName} Deleted");
            return branches;
        }

        public async Task<List<Branches>> GetAll()
        {
            _logger.LogInformation($"Branches retrieved");
            var branches = _context.Branches.ToList();
            if (branches != null)
                return branches;
            throw new NoBranchFoundException();
        }

        public async Task<Branches> GetByID(string key)
        {
            _logger.LogInformation($"Branch {key} retrieved");
            var branches = _context.Branches.SingleOrDefault(p => p.IFSCCode == key);
            if (branches != null)
                return branches;
            throw new NoBranchFoundException();
        }

        public async Task<Branches> Update(Branches item)
        {
            var branches = await GetByID(item.IFSCCode);
            _context.Entry<Branches>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation($"Branch {item.IFSCCode} Updated");
            return item;
        }
    }
}

