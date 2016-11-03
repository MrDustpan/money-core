using System.Threading.Tasks;
using Money.Boundary.Identity.Authenticate;

namespace Money.Domain.Identity.Authenticate
{
  public class AuthenticateHandler : IAuthenticateHandler
  {
    private readonly IUserRepository _userRepository;

    public AuthenticateHandler(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public async Task<AuthenticateResponse> Handle(AuthenticateRequest request)
    {
      var user = await _userRepository.GetUserByEmail(request.Email);

      if (user == null)
      {
        return new AuthenticateResponse { Status = AuthenticateStatus.UserNotFound };
      }

      return null;
    }
  }
}