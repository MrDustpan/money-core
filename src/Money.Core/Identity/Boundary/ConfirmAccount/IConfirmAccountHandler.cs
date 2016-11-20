using System.Threading.Tasks;

namespace Money.Core.Identity.Boundary.ConfirmAccount
{
  public interface IConfirmAccountHandler
  {
    Task<ConfirmAccountResponse> Handle(ConfirmAccountRequest request);
  }
}