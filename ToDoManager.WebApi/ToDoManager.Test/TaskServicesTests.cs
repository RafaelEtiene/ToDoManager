using Moq;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoManager.Application.Services;
using ToDoManager.Application.ViewModel;
using ToDoManager.Domain.Model;
using ToDoManager.Infrastructure.Interfaces;
using ToDoManager.Shared.Exceptions;
using Xunit;

namespace ToDoManager.Test
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _mapperMock = new Mock<IMapper>();
            _taskService = new TaskService(_taskRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetTasksAsync_ShouldReturnTaskViewModels_WhenTasksExist()
        {
            // Arrange
            var tasks = new List<TaskItem>
            {
                new TaskItem { Id = Guid.NewGuid(), Title = "Task 1", Description = "Description 1" },
                new TaskItem { Id = Guid.NewGuid(), Title = "Task 2", Description = "Description 2" }
            };

            var taskViewModels = new List<TaskViewModel>
            {
                new TaskViewModel { Id = tasks[0].Id, Title = tasks[0].Title, Description = tasks[0].Description },
                new TaskViewModel { Id = tasks[1].Id, Title = tasks[1].Title, Description = tasks[1].Description }
            };

            _taskRepositoryMock.Setup(repo => repo.GetTasksAsync()).ReturnsAsync(tasks);
            _mapperMock.Setup(m => m.Map<IEnumerable<TaskViewModel>>(It.IsAny<IEnumerable<TaskItem>>())).Returns(taskViewModels);

            // Act
            var result = await _taskService.GetTasksAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetTasksAsync_ShouldThrowBusinessException_WhenNoTasksExist()
        {
            // Arrange
            _taskRepositoryMock.Setup(repo => repo.GetTasksAsync()).ReturnsAsync(new List<TaskItem>());

            // Act & Assert
            var exception = await Assert.ThrowsAsync<BusinessException>(() => _taskService.GetTasksAsync());
            Assert.Equal("Tasks not found.", exception.Message);
        }

        [Fact]
        public async Task GetTaskById_ShouldReturnTaskViewModel_WhenTaskExists()
        {
            // Arrange
            var taskId = 1;
            var taskItem = new TaskItem { Id = Guid.NewGuid(), Title = "Task", Description = "Description" };
            var taskViewModel = new TaskViewModel { Id = taskItem.Id, Title = taskItem.Title, Description = taskItem.Description };

            _taskRepositoryMock.Setup(repo => repo.GetTaskByIdAsync(taskId)).ReturnsAsync(taskItem);
            _mapperMock.Setup(m => m.Map<TaskViewModel>(It.IsAny<TaskItem>())).Returns(taskViewModel);

            // Act
            var result = await _taskService.GetTaskById(taskId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(taskViewModel.Title, result.Title);
        }

        [Fact]
        public async Task InsertTaskAsync_ShouldCallInsertTaskRepositoryMethod()
        {
            // Arrange
            var taskViewModel = new InsertTaskViewModel { Title = "New Task", Description = "Description" };
            var taskItem = new TaskItem { Id = Guid.NewGuid(), Title = taskViewModel.Title, Description = taskViewModel.Description };

            _mapperMock.Setup(m => m.Map<TaskItem>(It.IsAny<InsertTaskViewModel>())).Returns(taskItem);

            // Act
            await _taskService.InsertTaskAsync(taskViewModel);

            // Assert
            _taskRepositoryMock.Verify(repo => repo.InsertTaskAsync(It.Is<TaskItem>(t => t.Title == taskViewModel.Title)), Times.Once);
        }

        [Fact]
        public async Task UpdateStateTaskAsync_ShouldCallUpdateStateRepositoryMethod()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var isCompleted = true;

            // Act
            await _taskService.UpdateStateTaskAsync(taskId, isCompleted);

            // Assert
            _taskRepositoryMock.Verify(repo => repo.UpdateStateTaskAsync(taskId, isCompleted), Times.Once);
        }

        [Fact]
        public async Task UpdateTaskAsync_ShouldCallUpdateTaskRepositoryMethod()
        {
            // Arrange
            var viewModel = new UpdateTaskViewModel { Id = Guid.NewGuid(), Title = "Updated Task", Description = "Updated Description" };

            // Act
            await _taskService.UpdateTaskAsync(viewModel);

            // Assert
            _taskRepositoryMock.Verify(repo => repo.UpdateTaskAsync(viewModel.Id, viewModel.Title, viewModel.Description), Times.Once);
        }

        [Fact]
        public async Task DeleteTaskAsync_ShouldCallDeleteTaskRepositoryMethod()
        {
            // Arrange
            var taskId = Guid.NewGuid();

            // Act
            await _taskService.DeleteTaskAsync(taskId);

            // Assert
            _taskRepositoryMock.Verify(repo => repo.DeleteTaskAsync(taskId), Times.Once);
        }
    }
}
