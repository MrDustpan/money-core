using MediatR;
using Money.Accounts.DomainModel;
using Money.Accounts.Messages;
using Money.Infrastructure;

namespace Money.Accounts.Handlers
{
  public class CreateAccountHandler : IRequestHandler<CreateAccount, CreateAccountResponse>
  {
    private readonly IRepository<Account> _accountRepository;

    public CreateAccountHandler(IRepository<Account> accountRepository)
    {
      _accountRepository = accountRepository;
    }

    public CreateAccountResponse Handle(CreateAccount request)
    {
      var account = new Account(request.Name, request.Balance);

      //_accountRepository.Add(account);

      return new CreateAccountResponse { Id = account.Id };
    }
  }
}