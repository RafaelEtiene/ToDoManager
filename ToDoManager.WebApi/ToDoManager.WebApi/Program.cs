using AutoMapper;
using FluentMigrator.Runner;
using Library.Application.Mapper;
using Microsoft.EntityFrameworkCore;
using ToDoManager.Application.Interfaces;
using ToDoManager.Infrastructure.Data.Migrations;
using ToDoManager.Infrastructure.Interfaces;
using ToDoManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var configuration = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<TaskProfile>();
});

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(runner => runner
        .AddSqlServer()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
        .ScanIn(typeof(CreateTables).Assembly).For.Migrations()
    )
    .AddLogging(logging => logging.AddConsole());

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// IoC
builder.Services.AddSingleton(configuration.CreateMapper());
builder.Services.AddScoped<ITaskService, ITaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
