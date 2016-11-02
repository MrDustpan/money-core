using System.Threading.Tasks;

namespace Money.Domain.Identity
{
  public interface IUserRepository
  {
    Task Add(User user);
    Task Update(User user);
    Task<User> GetUserByEmail(string email);
    Task<User> GetUserByConfirmationId(string email);
  }
}