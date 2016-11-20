using System.Threading.Tasks;

namespace Money.Core.Identity.Boundary.Authenticate
{
  public interface IAuthenticateHandler
  {
    Task<AuthenticateResponse> Handle(AuthenticateRequest request);
  }
}