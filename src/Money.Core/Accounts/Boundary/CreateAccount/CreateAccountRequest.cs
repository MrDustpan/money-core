namespace Money.Core.Accounts.Boundary.CreateAccount
{
  public class CreateAccountRequest
  {
    public int UserId { get; set; }
    public string Name { get; set; }
    public decimal CurrentBalance { get; set; }
  }
}