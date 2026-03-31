using TaskCQRS.Application.DTOs;
using TaskCQRS.Application.Mediator;

namespace TaskCQRS.Application.Queries;

public record GetTaskByIdQuery(int Id) : IQuery<TaskSummaryDto?>;

public record GetAllTasksQuery() : IQuery<IEnumerable<TaskSummaryDto>>;
