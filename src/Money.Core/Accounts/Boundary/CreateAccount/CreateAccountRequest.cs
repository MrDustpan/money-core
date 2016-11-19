namespace Money.Core.Accounts.Boundary.CreateAccount
{
  public class CreateAccountRequest
  {
    public string Name { get; set; }
    public decimal CurrentBalance { get; set; }
  }
}