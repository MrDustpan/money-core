namespace Money.Domain.Identity.Register
{
  public interface IPasswordHasher
  {
    string Hash(string password);
  }
}