namespace Money.Boundary.Identity.RegisterUser
{
  public class RegisterUserResponse
  {
    public RegisterUserStatus Status { get; set; }
    public int? UserId { get; set; }
  }

  public enum RegisterUserStatus
  {
    FailureEmailRequired,
    FailurePasswordRequirementsNotMet,
    Success
  }
}