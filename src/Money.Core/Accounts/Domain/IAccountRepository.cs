using System.Threading.Tasks;

namespace Money.Core.Accounts.Domain
{
  public interface IAccountRepository
  {
    Task Add(Account account);
  }
}