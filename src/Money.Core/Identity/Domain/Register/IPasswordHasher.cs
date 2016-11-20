namespace Money.Core.Identity.Domain.Register
{
  public interface IPasswordHasher
  {
    string Hash(string password);
  }
}