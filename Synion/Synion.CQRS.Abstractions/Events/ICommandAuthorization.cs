using System.Threading.Tasks;

namespace Synion.CQRS.Abstractions.Events
{
    public interface IEventAuthorization<TEvent>
        where TEvent : IEvent
    {
        Task<bool> IsAuthorized(string identity, TEvent command);
    }
}