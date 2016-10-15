using System;

namespace Money.Infrastructure
{
  public abstract class AggregateRoot
  {
    public Guid Id { get; set; }
  }
}