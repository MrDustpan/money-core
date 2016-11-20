using System.Threading.Tasks;

namespace Money.Core.Accounts.Boundary.GetAccountIndex
{
  public interface IGetAccountIndexHandler
  {
    Task<GetAccountIndexResponse> Handle(GetAccountIndexRequest request);
  }
}