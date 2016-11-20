using System.Threading.Tasks;
using Money.Core.Accounts.Boundary;

namespace Money.Core.Accounts.Domain
{
  public interface IAccountGateway
  {
    Task<GetAccountIndexResponse> GetAccountIndex(GetAccountIndexRequest request);
  }
}