using System.Threading.Tasks;
using Money.Core.Accounts.Boundary.GetAccountIndex;

namespace Money.Core.Accounts.Domain.GetAccountIndex
{
  public class GetAccountIndexHandler : IGetAccountIndexHandler
  {
    private readonly IAccountGateway _accountGateway;

    public GetAccountIndexHandler(IAccountGateway accountGateway)
    {
      _accountGateway = accountGateway;
    }

    public async Task<GetAccountIndexResponse> Handle(GetAccountIndexRequest request)
    {
      return await _accountGateway.GetAccountIndex(request);
    }
  }
}