using System.Threading.Tasks;
using Money.Boundary.Identity.Authenticate;

namespace Money.Domain.Identity.Authenticate
{
  public class AuthenticateHandler : IAuthenticateHandler
  {
    private readonly IUserRepository _userRepository;
    private readonly IPasswordValidator _passwordValidator;

    public AuthenticateHandler(IUserRepository userRepository, IPasswordValidator passwordValidator)
    {
      _userRepository = userRepository;
      _passwordValidator = passwordValidator;
    }

    public async Task<AuthenticateResponse> Handle(AuthenticateRequest request)
    {
      var user = await _userRepository.GetUserByEmail(request.Email);

      if (user == null)
      {
        return new AuthenticateResponse { Status = AuthenticateStatus.UserNotFound };
      }

      if (!_passwordValidator.IsValid(request.Password, user.Password))
      {
        return new AuthenticateResponse { Status = AuthenticateStatus.InvalidPassword };
      }

      return null;
    }
  }
}