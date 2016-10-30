using Money.Boundary.Identity.RegisterUser;

namespace Money.Web.Features.Auth.ViewModels
{
  public class RegisterViewModel
  {
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string ErrorMessage { get; set; }

    public RegisterViewModel() { }

    public void LoadResult(RegisterUserStatus status)
    {
      switch (status)
      {
        case RegisterUserStatus.FailureEmailRequired:
          ErrorMessage = "Email address is required.";
          break;

        case RegisterUserStatus.FailureEmailAlreadyExists:
          ErrorMessage = "An account already exists for that email.";
          break;

        case RegisterUserStatus.FailurePasswordRequirementsNotMet:
          ErrorMessage = "Password must be at least 8 characters.";
          break;

        case RegisterUserStatus.FailurePasswordAndConfirmDoNotMatch:
          ErrorMessage = "Password and Confirm Password do not match.";
          break;
      }
    }
  }
}