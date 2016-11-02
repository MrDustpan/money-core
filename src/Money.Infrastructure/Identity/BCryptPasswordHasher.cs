using Money.Domain.Identity.RegisterUser;

namespace Money.Infrastructure.Identity
{
  public class BCryptPasswordHasher : IPasswordHasher
  {
    public string Hash(string password)
    {
      return BCrypt.Net.BCrypt.HashPassword(password);
    }
  }
}