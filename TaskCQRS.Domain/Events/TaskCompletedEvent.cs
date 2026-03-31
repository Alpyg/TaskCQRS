namespace TaskCQRS.Domain.Events;

public record TaskCompletedEvent(int TaskId, DateTime CompletedAt);
