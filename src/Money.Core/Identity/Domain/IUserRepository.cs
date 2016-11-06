using System.Threading.Tasks;

namespace Money.Core.Identity.Domain
{
  public interface IUserRepository
  {
    Task Add(User user);
    Task Update(User user);
    Task<User> GetUserByEmail(string email);
    Task<User> GetUserByConfirmationId(string confirmationId);
  }
}