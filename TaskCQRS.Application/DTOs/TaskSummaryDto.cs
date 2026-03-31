namespace TaskCQRS.Application.DTOs;

public record TaskSummaryDto(
    int Id,
    string Title,
    string? Description,
    string Status,
    DateTime CreatedAt,
    DateTime? CompletedAt
);
