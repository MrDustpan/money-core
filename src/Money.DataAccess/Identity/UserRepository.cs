using System.Threading.Tasks;
using Money.Domain.Identity;

namespace Money.DataAccess.Identity
{
  public class UserRepository : IUserRepository
  {
    public async Task AddAsync(User user)
    {
      await Task.CompletedTask;
    }
  }
}