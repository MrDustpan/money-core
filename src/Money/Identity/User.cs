using Money.Infrastructure;

namespace Money.Identity
{
  public class User : AggregateRoot
  {
    public string Email { get; set; }
    public string Password { get; set; }
  }
}