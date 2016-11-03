using Money.Domain.Identity.Register;

namespace Money.Infrastructure.Identity
{
  public class BCryptPasswordHasher : IPasswordHasher, IPasswordValidator
  {
    public string Hash(string password)
    {
      return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool IsValid(string clearTextPassord, string hashedPassword)
    {
      return BCrypt.Net.Bcrypt.Verify(clearTextPassword, hashedPassword);
    }
  }
}