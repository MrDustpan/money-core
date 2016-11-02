using System.Threading.Tasks;

namespace Money.Boundary.Identity.RegisterUser
{
  public interface IConfirmAccountHandler
  {
    Task<ConfirmAccountResponse> HandleAsync(ConfirmAccountRequest request);
  }
}