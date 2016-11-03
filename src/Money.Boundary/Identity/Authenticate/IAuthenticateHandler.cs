using System.Threading.Tasks;

namespace Money.Boundary.Identity.Authenticate
{
  public interface IAuthenticateHandler
  {
    Task<AuthenticateResponse> Handle(AuthenticateRequest request);
  }
}