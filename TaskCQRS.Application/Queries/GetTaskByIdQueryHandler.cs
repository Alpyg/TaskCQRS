using TaskCQRS.Application.DTOs;
using TaskCQRS.Application.Mediator;
using TaskCQRS.Domain.Interfaces;

namespace TaskCQRS.Application.Queries;

public class GetTaskByIdQueryHandler : IQueryHandler<GetTaskByIdQuery, TaskSummaryDto?>
{
    private readonly ITaskRepository _repository;

    public GetTaskByIdQueryHandler(ITaskRepository repository) => _repository = repository;

    public async Task<TaskSummaryDto?> HandleAsync(
        GetTaskByIdQuery query,
        CancellationToken ct = default
    )
    {
        var task = await _repository.GetByIdAsync(query.Id, ct);

        if (task is null)
            return null;

        return new TaskSummaryDto(
            task.Id,
            task.Title,
            task.Description,
            task.Status.ToString(),
            task.CreatedAt,
            task.CompletedAt
        );
    }
}

public class GetAllTasksQueryHandler : IQueryHandler<GetAllTasksQuery, IEnumerable<TaskSummaryDto>>
{
    private readonly ITaskRepository _repository;

    public GetAllTasksQueryHandler(ITaskRepository repository) => _repository = repository;

    public async Task<IEnumerable<TaskSummaryDto>> HandleAsync(
        GetAllTasksQuery query,
        CancellationToken ct = default
    )
    {
        var tasks = await _repository.GetAllAsync(ct);

        return tasks.Select(task => new TaskSummaryDto(
            task.Id,
            task.Title,
            task.Description,
            task.Status.ToString(),
            task.CreatedAt,
            task.CompletedAt
        ));
    }
}
