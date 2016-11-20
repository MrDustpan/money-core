namespace Money.Core.Identity.Domain
{
  public interface IPasswordHasher
  {
    string Hash(string password);
  }
}