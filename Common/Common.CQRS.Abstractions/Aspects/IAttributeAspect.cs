using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions.Attributes;
using Common.CQRS.Abstractions.Commands;
using Common.CQRS.Abstractions.Events;
using Common.CQRS.Abstractions.Queries;

namespace Common.CQRS.Abstractions.Aspects
{
    public interface ICommandAttributeAspect<TCommand, in TAttribute>
        where TCommand : ICommand
        where TAttribute : AspectAttribute
    {
        Task Handle(TCommand command, TAttribute attribute, CancellationToken cancellationToken, CommandAspectDelegate next);
    }

    public interface IEventAttributeAspect<TEvent, in TAttribute>
        where TEvent : IEvent
        where TAttribute : AspectAttribute
    {
        Task Handle(TEvent command, TAttribute attribute, CancellationToken cancellationToken, EventAspectDelegate next);
    }
    
    public interface IQueryAttributeAspect<TQuery, TResponse, TAttribute>
        where TQuery : IQuery<TResponse>
        where TAttribute : AspectAttribute
    {
        Task<TResponse> Handle(TQuery query, TAttribute attribute, CancellationToken cancellationToken, QueryAspectDelegate<TResponse> next);
    }
}