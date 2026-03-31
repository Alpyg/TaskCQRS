using TaskCQRS.Domain.Events;
using TaskStatus = TaskCQRS.Domain.Enums.TaskStatus;

namespace TaskCQRS.Domain.Entities;

public class TaskItem
{
    public int Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public TaskStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    private readonly List<object> _domainEvents = new();
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    private TaskItem() { }

    public static TaskItem Create(string title, string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        var task = new TaskItem
        {
            Title = title,
            Description = description,
            Status = TaskStatus.Pending,
            CreatedAt = DateTime.UtcNow,
        };

        task._domainEvents.Add(new TaskCreatedEvent(task.Id, task.Title, task.CreatedAt));

        return task;
    }

    public void Start()
    {
        if (Status != TaskStatus.Pending)
            throw new InvalidOperationException("Only pending tasks can be started.");

        Status = TaskStatus.InProgress;
    }

    public void Complete()
    {
        if (Status == TaskStatus.Completed)
            throw new InvalidOperationException("Task is already completed.");

        Status = TaskStatus.Completed;
        CompletedAt = DateTime.UtcNow;

        _domainEvents.Add(new TaskCompletedEvent(Id, CompletedAt.Value));
    }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        Title = title;
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}
