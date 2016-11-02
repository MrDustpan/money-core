namespace Money.Domain.Identity.RegisterUser
{
  public interface IPasswordHasher
  {
    string Hash(string password);
  }
}