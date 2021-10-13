using System.Threading.Tasks;
using Common.CQRS.Abstractions.Events;

namespace Common.IAM.Abstractions.Events
{
    public interface IEventAuthorization<TEvent>
        where TEvent : IEvent
    {
        Task Authorize(string identity, TEvent command);
    }
}