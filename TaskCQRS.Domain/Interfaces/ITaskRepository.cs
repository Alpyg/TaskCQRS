using TaskCQRS.Domain.Entities;

namespace TaskCQRS.Domain.Interfaces;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(TaskItem task, CancellationToken cancellationToken = default);
    void Update(TaskItem task);
    void Delete(TaskItem task);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
