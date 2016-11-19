using System.Collections.Generic;
using System.Linq;
using Money.Core.Accounts.Boundary.GetAccountIndex;

namespace Money.Web.Features.Accounts.ViewModels
{
  public class AccountIndexViewModel
  {
    public List<AccountIndexItemViewModel> Accounts { get; set; }

    public AccountIndexViewModel() { }

    public AccountIndexViewModel(GetAccountIndexResponse response)
    {
      Accounts = response.Accounts.Select(x => new AccountIndexItemViewModel
      {
        Id = x.Id,
        Name = x.Name,
        Balance = x.Balance.ToString("C")
      }).ToList();
    }
  }
}