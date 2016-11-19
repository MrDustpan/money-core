namespace Money.Core.Accounts.Domain
{
  public class Account
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }
  }
}