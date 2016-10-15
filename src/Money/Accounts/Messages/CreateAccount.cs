using MediatR;

namespace Money.Accounts.Messages
{
  public class CreateAccount : IRequest<CreateAccountResponse>
  {
    public string Name { get; set; }
    public decimal Balance { get; set; }
  }
}