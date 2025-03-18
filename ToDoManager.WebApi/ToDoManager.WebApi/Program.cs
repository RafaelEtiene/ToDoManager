using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using System;
using ToDoManager.Infrastructure.Data.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(runner => runner
        .AddSqlServer()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
        .ScanIn(typeof(CreateTables).Assembly).For.Migrations()
    )
    .AddLogging(logging => logging.AddConsole());

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
