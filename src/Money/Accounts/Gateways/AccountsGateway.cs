using System.Collections.Generic;
using System.Linq;
using Money.Accounts.Models;

namespace Money.Accounts.Gateways
{
  public class AccountsGateway : IAccountsGateway
  {
    private static List<Account> _accounts = new List<Account>
    {
      new Account { AccountId = 1, Name = "Checking", Balance = 50.00m },
      new Account { AccountId = 2, Name = "Savings", Balance = 99.99m }
    };

    public List<AccountOverview> GetAllAccountOverviews()
    {
      return _accounts.Select(x => new AccountOverview
      {
        AccountId = x.AccountId,
        Name = x.Name,
        Balance = x.Balance
      }).ToList();
    }    
  }

  public interface IAccountsGateway
  {
    List<AccountOverview> GetAllAccountOverviews();
  }

  public class Account
  {
    public int AccountId { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
  }
}