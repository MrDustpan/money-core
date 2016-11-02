namespace Money.Boundary.Identity.RegisterUser
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