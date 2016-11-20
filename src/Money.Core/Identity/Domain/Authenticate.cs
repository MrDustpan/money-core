using System.Threading.Tasks;
using Money.Core.Identity.Boundary;

namespace Money.Core.Identity.Domain
{
  public class Authenticate : IAuthenticate
  {
    private readonly IUserRepository _userRepository;
    private readonly IPasswordValidator _passwordValidator;

    public Authenticate(IUserRepository userRepository, IPasswordValidator passwordValidator)
    {
      _userRepository = userRepository;
      _passwordValidator = passwordValidator;
    }

    public async Task<AuthenticateResponse> Execute(AuthenticateRequest request)
    {
      var user = await _userRepository.GetUserByEmail(request.Email);

      if (user == null)
      {
        return new AuthenticateResponse { Status = AuthenticateStatus.UserNotFound };
      }

      if (user.Status == UserStatus.Locked)
      {
        return new AuthenticateResponse { Status = AuthenticateStatus.AccountLocked };
      }

      if (!_passwordValidator.IsValid(request.Password, user.Password))
      {
        return await FailedLogin(user);
      }

      return await SuccessfulLogin(user);
    }

    private async Task<AuthenticateResponse> FailedLogin(User user)
    {
      var status = await UpdateFailedAttempts(user);

      return new AuthenticateResponse { Status = status };
    }

    private async Task<AuthenticateStatus> UpdateFailedAttempts(User user)
    {
      user.FailedAttempts++;

      var status = AuthenticateStatus.InvalidPassword;
      if (user.FailedAttempts >= 5)
      {
        status = AuthenticateStatus.AccountLocked;
        user.Status = UserStatus.Locked;
      }

      await _userRepository.Update(user);

      return status;
    }

    private async Task<AuthenticateResponse> SuccessfulLogin(User user)
    {
      await ResetFailedAttempts(user);

      return new AuthenticateResponse
      {
        Status = AuthenticateStatus.Success,
        UserId = user.Id
      };
    }

    private async Task ResetFailedAttempts(User user)
    {
      if (user.FailedAttempts == 0)
      {
        return;
      }

      user.FailedAttempts = 0;
      user.Status = UserStatus.Confirmed;
      await _userRepository.Update(user);
    }
  }
}