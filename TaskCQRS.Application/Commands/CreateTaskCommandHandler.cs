using TaskCQRS.Application.Mediator;
using TaskCQRS.Domain.Entities;
using TaskCQRS.Domain.Interfaces;

namespace TaskCQRS.Application.Commands;

public class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand, AckResponse>
{
    private readonly ITaskRepository _repository;

    public CreateTaskCommandHandler(ITaskRepository repository) => _repository = repository;

    public async Task<AckResponse> HandleAsync(
        CreateTaskCommand command,
        CancellationToken ct = default
    )
    {
        var task = TaskItem.Create(command.Title, command.Description);

        await _repository.AddAsync(task, ct);
        await _repository.SaveChangesAsync(ct);

        return new AckResponse(Guid.NewGuid(), "Task creation accepted.");
    }
}
