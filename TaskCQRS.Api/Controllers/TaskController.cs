using Microsoft.AspNetCore.Mvc;
using TaskCQRS.Application.Commands;
using TaskCQRS.Application.DTOs;
using TaskCQRS.Application.Mediator;
using TaskCQRS.Application.Queries;

namespace TaskCQRS.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(AckResponse), StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Create(
        [FromBody] CreateTaskCommand command,
        CancellationToken ct
    )
    {
        var result = await _mediator.SendCommandAsync<CreateTaskCommand, AckResponse>(command, ct);
        return Accepted(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TaskSummaryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await _mediator.SendQueryAsync<GetTaskByIdQuery, TaskSummaryDto?>(
            new GetTaskByIdQuery(id),
            ct
        );
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaskSummaryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _mediator.SendQueryAsync<GetAllTasksQuery, IEnumerable<TaskSummaryDto>>(
            new GetAllTasksQuery(),
            ct
        );
        return Ok(result);
    }
}
