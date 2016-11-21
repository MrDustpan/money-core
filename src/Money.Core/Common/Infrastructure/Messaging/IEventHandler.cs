using System.Threading.Tasks;

namespace Money.Core.Common.Infrastructure.Messaging
{
  public interface IEventHandler<T> where T : IEvent
  {
    Task Handle(T @event);
  }
}