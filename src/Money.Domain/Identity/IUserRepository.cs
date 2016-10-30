using System.Threading.Tasks;

namespace Money.Domain.Identity
{
  public interface IUserRepository
  {
    Task AddAsync(User user);
    Task<User> GetUserByEmailAsync(string email);
  }
}