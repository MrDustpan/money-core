using System.Threading.Tasks;
using Money.Core.Accounts.Boundary;

namespace Money.Core.Accounts.Domain
{
  public class CreateAccount : ICreateAccount
  {
    private readonly IAccountRepository _accountRepository;

    public CreateAccount(IAccountRepository accountRepository)
    {
      _accountRepository = accountRepository;
    }

    public async Task<CreateAccountResponse> Execute(CreateAccountRequest request)
    {
      var account = new Account
      {
        UserId = request.UserId,
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