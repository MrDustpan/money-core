using System.Threading.Tasks;

namespace Money.Boundary.Identity.RegisterUser
{
  public interface IConfirmAccountHandler
  {
    Task<ConfirmAccountResponse> Handle(ConfirmAccountRequest request);
  }
}