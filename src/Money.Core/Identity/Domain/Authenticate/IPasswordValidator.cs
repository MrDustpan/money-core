namespace Money.Core.Identity.Domain.Authenticate
{
  public interface IPasswordValidator
  {
    bool IsValid(string clearTextPassword, string hashedPassword);
  }
}