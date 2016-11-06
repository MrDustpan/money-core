namespace Money.Core.Identity.Boundary.Register
{
  public class RegisterResponse
  {
    public RegisterStatus Status { get; set; }
    public int? UserId { get; set; }
  }

  public enum RegisterStatus
  {
    FailureEmailRequired,
    FailurePasswordRequirementsNotMet,
    FailurePasswordAndConfirmDoNotMatch,
    FailureEmailAlreadyExists,
    Success
  }
}