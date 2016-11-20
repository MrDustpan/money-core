using System.Collections.Generic;
using System.Threading.Tasks;

namespace Money.Core.Accounts.Boundary
{
  public interface IGetAccountIndex
  {
    Task<GetAccountIndexResponse> Execute(GetAccountIndexRequest request);
  }

  public class GetAccountIndexRequest
  {
    public int UserId { get; set; }
  }

  public class GetAccountIndexResponse
  {
    public List<AccountIndexItem> Accounts { get; set; }
  }

  public class AccountIndexItem
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
  }
}