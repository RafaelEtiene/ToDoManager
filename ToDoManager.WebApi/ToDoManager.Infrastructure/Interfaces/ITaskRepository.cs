using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoManager.Domain.Model;

namespace ToDoManager.Infrastructure.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetTasksAsync();
        Task<TaskItem> GetTaskById(int IdTask);
    }
}
