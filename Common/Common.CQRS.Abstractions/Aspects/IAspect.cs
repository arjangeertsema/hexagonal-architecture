using System.Threading;
using System.Threading.Tasks;
using Common.CQRS.Abstractions.Commands;
using Common.CQRS.Abstractions.Events;
using Common.CQRS.Abstractions.Queries;

namespace Common.CQRS.Abstractions.Aspects
{
    public delegate Task CommandAspectDelegate();
    
    public interface ICommandAspect<in TCommand>
        where TCommand : notnull, ICommand
    {
        Task Handle(TCommand command, IAttributeCollection attributeCollection, CancellationToken cancellationToken, CommandAspectDelegate next);
    }

    public delegate Task EventAspectDelegate();
    
    public interface IEventAspect<in TEvent>
        where TEvent : notnull, IEvent
    {
        Task Handle(TEvent command, IAttributeCollection attributeCollection, CancellationToken cancellationToken, EventAspectDelegate next);
    }
    public delegate Task<TResponse> QueryAspectDelegate<TResponse>();
    
    public interface IQueryAspect<in TQuery, TResponse>
        where TQuery : notnull, IQuery<TResponse>
    {
        Task<TResponse> Handle(TQuery query, IAttributeCollection attributeCollection, CancellationToken cancellationToken, QueryAspectDelegate<TResponse> next);
    }
}