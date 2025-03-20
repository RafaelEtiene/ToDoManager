using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoManager.Domain.Model;
using ToDoManager.Infrastructure.Interfaces;
using ToDoManager.Shared.Exceptions;

namespace ToDoManager.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public Task<TaskItem> GetTaskByIdAsync(int IdTask)
        {
            throw new NotImplementedException();
        }

        public async Task InsertTaskAsync(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStateTaskAsync(Guid idTask, bool isCompleted)
        {
            var task = await _context.Tasks.FindAsync(idTask);

            if(task == null)
            {
                throw new BusinessException($"Task with id {idTask} not found.");
            }

            task.IsCompleted = isCompleted;
            await _context.SaveChangesAsync();
        }
    }
}
