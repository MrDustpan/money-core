using System.Threading.Tasks;
using Money.Core.Accounts.Boundary;

namespace Money.Core.Accounts.Domain
{
  public class GetAccountIndex : IGetAccountIndex
  {
    private readonly IAccountGateway _accountGateway;

    public GetAccountIndex(IAccountGateway accountGateway)
    {
      _accountGateway = accountGateway;
    }

    public async Task<GetAccountIndexResponse> Execute(GetAccountIndexRequest request)
    {
      return await _accountGateway.GetAccountIndex(request);
    }
  }
}