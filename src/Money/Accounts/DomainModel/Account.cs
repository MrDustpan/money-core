using Money.Infrastructure;

namespace Money.Accounts.DomainModel
{
  public class Account : AggregateRoot
  {
    public string Name { get; set; }
    public decimal Balance { get; private set; }

    public Account() { }

    public Account(string name, decimal balance)
    {
      Name = name;
      Balance = balance;
    }
  }
}