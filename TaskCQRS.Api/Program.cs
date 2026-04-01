using Microsoft.EntityFrameworkCore;
using TaskCQRS.Application.Commands;
using TaskCQRS.Application.DTOs;
using TaskCQRS.Application.Mediator;
using TaskCQRS.Application.Queries;
using TaskCQRS.Domain.Interfaces;
using TaskCQRS.Infrastructure.Persistence;
using TaskCQRS.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        Environment.GetEnvironmentVariable("TASKCQRS_DB")
            ?? throw new InvalidOperationException("TASKCQRS_DB environment variable is not set.")
    )
);

builder.Services.AddScoped<IMediator, Mediator>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<
    ICommandHandler<CreateTaskCommand, AckResponse>,
    CreateTaskCommandHandler
>();
builder.Services.AddScoped<
    IQueryHandler<GetTaskByIdQuery, TaskSummaryDto?>,
    GetTaskByIdQueryHandler
>();
builder.Services.AddScoped<
    IQueryHandler<GetAllTasksQuery, IEnumerable<TaskSummaryDto>>,
    GetAllTasksQueryHandler
>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment()) { }

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
