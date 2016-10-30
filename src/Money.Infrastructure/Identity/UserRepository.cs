using System.Threading.Tasks;
using Money.Domain.Identity;

namespace Money.Infrastructure.Identity
{
  public class UserRepository : IUserRepository
  {
    public async Task AddAsync(User user)
    {
      await Task.CompletedTask;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
      return await Task.FromResult((User)null);
    }
  }
}