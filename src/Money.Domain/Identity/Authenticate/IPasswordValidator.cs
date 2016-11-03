namespace Money.Domain.Identity.Authenticate
{
  public interface IPasswordValidator
  {
    bool IsValid(string clearTextPassword, string hashedPassword);
  }
}