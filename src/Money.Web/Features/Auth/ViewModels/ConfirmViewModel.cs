using Money.Core.Identity.Boundary.ConfirmAccount;

namespace Money.Web.Features.Auth.ViewModels
{
  public class ConfirmViewModel
  {
    public string Message { get; set; }
    public string Class { get; set; }

    public ConfirmViewModel()
    {
      Message = "Check your email for instructions on how to confirm your account.";
      Class = "primary";
    }

    public ConfirmViewModel(ConfirmAccountStatus status)
    {
      switch (status)
      {
        case ConfirmAccountStatus.FailureConfirmationIdNotFound:
          Message = "Oops, we were unable to confirm your account.";
          Class = "danger";
          break;

        default:
          Message = "Your account has been confirmed!";
          Class = "success";
          break;
      }
    }
  }
}