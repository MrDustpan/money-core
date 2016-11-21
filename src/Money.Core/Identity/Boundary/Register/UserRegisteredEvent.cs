using Money.Core.Common.Infrastructure.Messaging;

namespace Money.Core.Identity.Boundary.Register
{
  public class UserRegisteredEvent : IEvent
  {
    public int UserId { get; set; }
  }
}