using System.Threading.Tasks;

namespace Money.Core.Identity.Boundary.Register
{
  public interface IConfirmAccountHandler
  {
    Task<ConfirmAccountResponse> Handle(ConfirmAccountRequest request);
  }
}