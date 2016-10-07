using Money.Accounts.Messages;
using MediatR;
using System.Collections.Generic;
using Money.Accounts.Gateways;
using System.Linq;

namespace Money.Accounts.Handlers
{
  public class GetAccountIndexHandler : IRequestHandler<GetAccountIndex, GetAccountIndexResponse>
  {
    private readonly IAccountsGateway _accountsGateway;

    public GetAccountIndexHandler(IAccountsGateway accountsGateway)
    {
      _accountsGateway = accountsGateway;
    }

    public GetAccountIndexResponse Handle(GetAccountIndex request)
    {
      var accounts = _accountsGateway.GetAllAccountOverviews();
      
      return new GetAccountIndexResponse
      {
        Accounts = accounts.Select(x => new AccountIndexItem
        {
          AccountId = x.AccountId,
          Name = x.Name
        }).ToList()
      };
    }
  }
}