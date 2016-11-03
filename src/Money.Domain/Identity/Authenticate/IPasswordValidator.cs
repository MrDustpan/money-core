namespace Money.Domain.Identity.Authenticate
{
  public interface IPasswordValidator
  {
    bool IsValid(string password, string hashedPassword);
  }
}