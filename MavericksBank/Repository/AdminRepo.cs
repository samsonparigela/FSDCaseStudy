using System;
using MavericksBank.Contexts;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Repository
{
	public class AdminRepo:IRepository<Admin,int>
	{
        private readonly ILogger<AdminRepo> _logger;
        private readonly RequestTrackerContext _context;
        public AdminRepo(ILogger<AdminRepo> logger, RequestTrackerContext context)
        {
            _logger = logger;
            _context = context;

        }

        public async Task<Admin> Add(Admin item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation($"Admin {item.AdminID} Added");
            return item;
        }

        public async Task<Admin> Delete(int item)
        {
            Admin admin = await GetByID(item);
            _context.Remove(admin);
            _context.SaveChanges();
            _logger.LogInformation($"Admin {admin.AdminID} Deleted");
            return admin;
        }

        public async Task<List<Admin>> GetAll()
        {
            _logger.LogInformation($"Admins retrieved");
            var admin = _context.Admin.ToList();
            if (admin != null)
                return admin;
            throw new NoAdminFoundException();
        }

        public async Task<Admin> GetByID(int key)
        {
            _logger.LogInformation($"Admin {key} retrieved");
            var admin = _context.Admin.SingleOrDefault(p => p.AdminID == key);
            if (admin != null)
                return admin;
            throw new NoAdminFoundException();
        }

        public async Task<Admin> Update(Admin item)
        {
            var admin = await GetByID(item.AdminID);
            _context.Entry<Admin>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation($"Admin {item.AdminID} Updated");
            return item;
        }
    }
}

