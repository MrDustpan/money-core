using System;
using System.Threading.Tasks;
using Money.Core.Common.Infrastructure.Messaging;
using Money.Core.Identity.Boundary.Register;

namespace Money.Core.Budgets.Domain.NewBudgetSetup
{
    public class CreateDefaultCategories : IEventHandler<UserRegisteredEvent>
    {
      public async Task Handle(UserRegisteredEvent @event)
      {
        Console.WriteLine("Creating default categories");
        await Task.CompletedTask;
      }
    }
}