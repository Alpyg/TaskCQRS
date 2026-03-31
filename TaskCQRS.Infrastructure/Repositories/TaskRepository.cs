using Microsoft.EntityFrameworkCore;
using TaskCQRS.Domain.Entities;
using TaskCQRS.Domain.Interfaces;
using TaskCQRS.Infrastructure.Persistence;

namespace TaskCQRS.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _db;

    public TaskRepository(AppDbContext db) => _db = db;

    public async Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _db.Tasks.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        return await _db.Tasks.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        await _db.Tasks.AddAsync(task, cancellationToken);
    }

    public void Update(TaskItem task)
    {
        _db.Tasks.Update(task);
    }

    public void Delete(TaskItem task)
    {
        _db.Tasks.Remove(task);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _db.SaveChangesAsync(cancellationToken);
    }
}
