using System.Threading.Tasks;
using Money.Core.Accounts.Boundary.CreateAccount;

namespace Money.Core.Accounts.Domain.CreateAccount
{
  public class CreateAccountHandler : ICreateAccountHandler
  {
    private readonly IAccountRepository _accountRepository;

    public CreateAccountHandler(IAccountRepository accountRepository)
    {
      _accountRepository = accountRepository;
    }

    public async Task<CreateAccountResponse> Handle(CreateAccountRequest request)
    {
      var account = new Account
      {
        Name = request.Name,
        Balance = request.CurrentBalance
      };

      await _accountRepository.Add(account);

      return new CreateAccountResponse
      {
        AccountId = account.Id
      };
    }
  }
}