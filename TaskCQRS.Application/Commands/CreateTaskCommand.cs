using TaskCQRS.Application.Mediator;

namespace TaskCQRS.Application.Commands;

public record CreateTaskCommand(string Title, string? Description) : ICommand<AckResponse>;

public record AckResponse(Guid CorrelationId, string Message);
