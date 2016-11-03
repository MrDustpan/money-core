using System.Threading.Tasks;

namespace Money.Boundary.Identity.Register
{
  public interface IConfirmAccountHandler
  {
    Task<ConfirmAccountResponse> Handle(ConfirmAccountRequest request);
  }
}