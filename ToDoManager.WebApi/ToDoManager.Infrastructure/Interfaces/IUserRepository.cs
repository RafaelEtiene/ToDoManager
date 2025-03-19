using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoManager.Domain.Model;

namespace ToDoManager.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByNameAsync(string name);
        Task InsertUserAsync(User user);
    }
}
