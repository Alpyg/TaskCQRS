using Microsoft.Extensions.DependencyInjection;

namespace TaskCQRS.Application.Mediator;

public interface IMediator
{
    Task<TResult> SendCommandAsync<TCommand, TResult>(
        TCommand command,
        CancellationToken ct = default
    )
        where TCommand : ICommand<TResult>;

    Task<TResult> SendQueryAsync<TQuery, TResult>(TQuery query, CancellationToken ct = default)
        where TQuery : IQuery<TResult>;
}

public class Mediator : IMediator
{
    private readonly IServiceProvider _services;

    public Mediator(IServiceProvider services) => _services = services;

    public Task<TResult> SendCommandAsync<TCommand, TResult>(
        TCommand command,
        CancellationToken ct = default
    )
        where TCommand : ICommand<TResult>
    {
        var handler = _services.GetRequiredService<ICommandHandler<TCommand, TResult>>();
        return handler.HandleAsync(command, ct);
    }

    public Task<TResult> SendQueryAsync<TQuery, TResult>(
        TQuery query,
        CancellationToken ct = default
    )
        where TQuery : IQuery<TResult>
    {
        var handler = _services.GetRequiredService<IQueryHandler<TQuery, TResult>>();
        return handler.HandleAsync(query, ct);
    }
}
