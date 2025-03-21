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
        Task<TaskItem> GetTaskByIdAsync(int IdTask);
        Task InsertTaskAsync(TaskItem task);
        Task UpdateStateTaskAsync(Guid idTask, bool isCompleted);
        Task UpdateTaskAsync(Guid id, string title, string description);
        Task DeleteTaskAsync(Guid id);
    }
}
