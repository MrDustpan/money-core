using Money.Core.Identity.Boundary;

namespace Money.Web.Features.Auth.ViewModels
{
  public class RegisterViewModel
  {
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string ErrorMessage { get; set; }

    public RegisterViewModel() { }

    public void LoadResult(RegisterStatus status)
    {
      switch (status)
      {
        case RegisterStatus.FailureEmailRequired:
          ErrorMessage = "Email address is required.";
          break;

        case RegisterStatus.FailureEmailAlreadyExists:
          ErrorMessage = "An account already exists for that email.";
          break;

        case RegisterStatus.FailurePasswordRequirementsNotMet:
          ErrorMessage = "Password must be at least 8 characters.";
          break;

        case RegisterStatus.FailurePasswordAndConfirmDoNotMatch:
          ErrorMessage = "Password and Confirm Password do not match.";
          break;
      }
    }
  }
}