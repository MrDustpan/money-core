using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Money.Core.Common.Infrastructure.Messaging
{
  public class ServiceBus : IServiceBus
  {
    public static IServiceProvider ServiceProvider;

    public async Task RaiseEvent<T>(T @event) where T : IEvent
    {
      if (ServiceProvider == null)
      {
        return;
      }

      foreach(var handler in ServiceProvider.GetServices<IEventHandler<T>>())
      {
        await handler.Handle(@event);
      }
    }
  }
}