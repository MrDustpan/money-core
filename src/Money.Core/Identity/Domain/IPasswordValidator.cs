namespace Money.Core.Identity.Domain
{
  public interface IPasswordValidator
  {
    bool IsValid(string clearTextPassword, string hashedPassword);
  }
}