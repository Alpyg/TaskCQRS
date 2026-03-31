namespace TaskCQRS.Domain.Events;

public record TaskCreatedEvent(int TaskId, string Title, DateTime CreatedAt);
