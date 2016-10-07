using MediatR;

namespace Money.Accounts.Messages
{
  public class GetAccountIndex : IRequest<GetAccountIndexResponse> { }
}