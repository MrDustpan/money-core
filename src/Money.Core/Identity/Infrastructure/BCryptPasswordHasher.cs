using Money.Core.Identity.Domain.Authenticate;
using Money.Core.Identity.Domain.Register;

namespace Money.Core.Identity.Infrastructure
{
  public class BCryptPasswordHasher : IPasswordHasher, IPasswordValidator
  {
    public string Hash(string password)
    {
      return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool IsValid(string clearTextPassword, string hashedPassword)
    {
      return BCrypt.Net.BCrypt.Verify(clearTextPassword, hashedPassword);
    }
  }
}