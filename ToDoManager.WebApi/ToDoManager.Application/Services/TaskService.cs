using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoManager.Application.Interfaces;
using ToDoManager.Application.ViewModel;
using ToDoManager.Domain.Model;
using ToDoManager.Infrastructure.Interfaces;
using ToDoManager.Shared.Exceptions;

namespace ToDoManager.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskViewModel>> GetTasksAsync()
        {
            var tasks = await _taskRepository.GetTasksAsync();

            if (!tasks.Any())
            {
                throw new BusinessException("Tasks not found.");
            }

            return _mapper.Map<IEnumerable<TaskViewModel>>(tasks);
        }

        public Task<TaskViewModel> GetTaskById(int IdTask)
        {
            throw new NotImplementedException();
        }

        public async Task InsertTaskAsync(TaskViewModel task)
        {
            await _taskRepository.InsertTaskAsync(_mapper.Map<TaskItem>(task));
        }
    }
}
