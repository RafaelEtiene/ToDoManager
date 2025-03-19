using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoManager.Domain.Model;
using ToDoManager.Infrastructure.Interfaces;

namespace ToDoManager.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetUserByNameAsync(string name)
        {
            return await _context.Users
                .FirstOrDefaultAsync(e => e.Username.ToLower().Equals(name.ToLower()));
        }

        public async Task InsertUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
