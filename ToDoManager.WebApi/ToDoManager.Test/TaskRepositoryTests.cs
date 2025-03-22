using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoManager.Domain.Model;
using ToDoManager.Infrastructure.Repositories;
using ToDoManager.Shared.Exceptions;
using Xunit;

namespace ToDoManager.Test
{
    public class TaskRepositoryTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetTasksAsync_ShouldReturnEmptyList_WhenNoTasksExist()
        {
            var context = GetDbContext();
            var repository = new TaskRepository(context);

            var result = await repository.GetTasksAsync();
            Assert.Empty(result);
        }

        [Fact]
        public async Task InsertTaskAsync_ShouldAddTaskToDatabase()
        {
            var context = GetDbContext();
            var repository = new TaskRepository(context);
            var task = new TaskItem { Id = Guid.NewGuid(), Title = "Test Task", Description = "Test Desc", IsCompleted = false };

            await repository.InsertTaskAsync(task);

            var storedTask = await context.Tasks.FindAsync(task.Id);
            Assert.NotNull(storedTask);
            Assert.Equal("Test Task", storedTask.Title);
        }

        [Fact]
        public async Task UpdateStateTaskAsync_ShouldUpdateTaskState()
        {
            var context = GetDbContext();
            var repository = new TaskRepository(context);
            var task = new TaskItem { Id = Guid.NewGuid(), Title = "Test Task", IsCompleted = false };
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            await repository.UpdateStateTaskAsync(task.Id, true);
            var updatedTask = await context.Tasks.FindAsync(task.Id);

            Assert.True(updatedTask.IsCompleted);
        }

        [Fact]
        public async Task UpdateStateTaskAsync_ShouldThrowException_WhenTaskNotFound()
        {
            var context = GetDbContext();
            var repository = new TaskRepository(context);

            await Assert.ThrowsAsync<BusinessException>(async () =>
                await repository.UpdateStateTaskAsync(Guid.NewGuid(), true));
        }

        [Fact]
        public async Task UpdateTaskAsync_ShouldUpdateTaskDetails()
        {
            var context = GetDbContext();
            var repository = new TaskRepository(context);
            var task = new TaskItem { Id = Guid.NewGuid(), Title = "Old Title", Description = "Old Desc" };
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            await repository.UpdateTaskAsync(task.Id, "New Title", "New Desc");
            var updatedTask = await context.Tasks.FindAsync(task.Id);

            Assert.Equal("New Title", updatedTask.Title);
            Assert.Equal("New Desc", updatedTask.Description);
        }

        [Fact]
        public async Task DeleteTaskAsync_ShouldRemoveTaskFromDatabase()
        {
            var context = GetDbContext();
            var repository = new TaskRepository(context);
            var task = new TaskItem { Id = Guid.NewGuid(), Title = "Task to delete" };
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            await repository.DeleteTaskAsync(task.Id);
            var deletedTask = await context.Tasks.FindAsync(task.Id);

            Assert.Null(deletedTask);
        }

        [Fact]
        public async Task DeleteTaskAsync_ShouldThrowException_WhenTaskNotFound()
        {
            var context = GetDbContext();
            var repository = new TaskRepository(context);

            await Assert.ThrowsAsync<BusinessException>(async () =>
                await repository.DeleteTaskAsync(Guid.NewGuid()));
        }
    }
}
