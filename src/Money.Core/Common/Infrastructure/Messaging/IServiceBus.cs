using System.Threading.Tasks;

namespace Money.Core.Common.Infrastructure.Messaging
{
  public interface IServiceBus
  {
    Task RaiseEvent<T>(T @event) where T : IEvent;
  }
}