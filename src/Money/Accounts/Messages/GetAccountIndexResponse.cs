using System.Collections.Generic;

namespace Money.Accounts.Messages
{
  public class GetAccountIndexResponse
  {
    public List<AccountIndexItem> Accounts { get; set; }
  }

  public class AccountIndexItem
  {
    public int AccountId { get; set; }
    public string Name { get; set; }
  }
}