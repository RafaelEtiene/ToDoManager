using AutoMapper;
using FluentMigrator.Runner;
using Library.Application.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoManager.Application.Interfaces;
using ToDoManager.Application.Services;
using ToDoManager.Infrastructure.Data.Migrations;
using ToDoManager.Infrastructure.Interfaces;
using ToDoManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var configuration = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<TaskProfile>();
    cfg.AddProfile<UserProfile>();
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
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

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
app.UseAuthentication();
app.MapControllers();

app.Run();
