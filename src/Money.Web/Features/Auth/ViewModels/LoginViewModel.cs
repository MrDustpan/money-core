using Money.Core.Identity.Boundary.Authenticate;

namespace Money.Web.Features.Auth.ViewModels
{
  public class LoginViewModel
  {
    public string Email { get; set; }
    public string Password { get; set; }
    public string ErrorMessage { get; set; }

    public LoginViewModel() { }

    public void LoadResult(AuthenticateStatus status)
    {
      switch (status)
      {
        case AuthenticateStatus.InvalidPassword:
        case AuthenticateStatus.UserNotFound:
        case AuthenticateStatus.AccountLocked:
          ErrorMessage = "Login failed.";
          break;
      }
    }
  }
}