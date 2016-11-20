using System.Collections.Generic;
using System.Linq;
using Money.Core.Accounts.Boundary;

namespace Money.Web.Features.Accounts.ViewModels
{
  public class AccountTransactionsViewModel
  {
    public string AccountName { get; set; }
    public List<AccountIndexItemViewModel> AccountsIndex { get; set; }

    public AccountTransactionsViewModel() { }

    public AccountTransactionsViewModel(int accountId, GetAccountIndexResponse index)
    {
      AccountName = index.Accounts.SingleOrDefault(x => x.Id == accountId)?.Name;
      
      AccountsIndex = index.Accounts.Select(x => new AccountIndexItemViewModel
      {
        Id = x.Id,
        Name = x.Name,
        Balance = x.Balance.ToString("C"),
        Selected = x.Id == accountId
      }).ToList();
    }
  }

  public class AccountIndexItemViewModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Balance { get; set; }
    public bool Selected { get; set; }
  }
}