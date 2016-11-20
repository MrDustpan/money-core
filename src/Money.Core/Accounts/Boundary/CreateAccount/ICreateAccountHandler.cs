using System.Threading.Tasks;

namespace Money.Core.Accounts.Boundary.CreateAccount
{
  public interface ICreateAccountHandler
  {
    Task<CreateAccountResponse> Handle(CreateAccountRequest request);
  }
}