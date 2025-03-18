using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoManager.Application.ViewModel;

namespace ToDoManager.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskViewModel>> GetTasksAsync();
        Task<TaskViewModel> GetTaskById(int IdTask);
    }
}
