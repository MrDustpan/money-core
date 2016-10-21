using System.Threading.Tasks;

namespace Money.Domain.Identity.Entities
{
  public interface IUserRepository
  {
    Task AddAsync(User user);
  }
}