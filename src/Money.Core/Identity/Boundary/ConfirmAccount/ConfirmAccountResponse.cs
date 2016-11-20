namespace Money.Core.Identity.Boundary.ConfirmAccount
{
  public class ConfirmAccountResponse
  {
    public ConfirmAccountStatus Status { get; set; }
  }

  public enum ConfirmAccountStatus
  {
    FailureConfirmationIdNotFound,
    FailureAlreadyConfirmed,
    Success
  }
}