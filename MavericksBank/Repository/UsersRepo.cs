using System;
using MavericksBank.Contexts;
using MavericksBank.Exceptions;
using MavericksBank.Interfaces;
using MavericksBank.Models;
using Microsoft.EntityFrameworkCore;

namespace MavericksBank.Repository
{
	public class UsersRepo:IRepository<Users,string>
	{
        private readonly ILogger<UsersRepo> _logger;
        private readonly RequestTrackerContext _context;
        public UsersRepo(ILogger<UsersRepo> logger, RequestTrackerContext context)
        {
            _logger = logger;
            _context = context;

        }

        public async Task<Users> Add(Users item)
        {
            _context.Add(item);
            _context.SaveChanges();
            _logger.LogInformation($"User {item.UserID} Added");
            return item;
        }

        public async Task<Users> Delete(string key)
        {
            Users user = await GetByID(key);
            if (user == null)
                throw new NoUserFoundException();
            _context.Remove(user);
            _context.SaveChanges();
            _logger.LogInformation($"User {user.UserID} Deleted");
            return user;
        }

        public async Task<List<Users>> GetAll()
        {
            _logger.LogInformation($"Users retrieved");
            var users = _context.Users.ToList();
            if (users != null)
                return users;
            throw new NoUserFoundException();
        }

        public async Task<Users> GetByID(string key)
        {
            _logger.LogInformation($"User {key} retrieved");
            var user = _context.Users.SingleOrDefault(p => p.UserName == key);
            if (user != null)
                return user;
            else
                return null;
        }

        public async Task<Users> Update(Users item)
        {
            var user = await GetByID(item.UserName);
            if (user == null)
                throw new NoUserFoundException();
            _context.Entry<Users>(item).State = EntityState.Modified;
            _context.SaveChanges();
            _logger.LogInformation($"User {item.UserID} Updated");
            return user;
        }
    }
}

