namespace Money.Core.Identity.Boundary.Register
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