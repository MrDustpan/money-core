namespace Money.Domain.Identity.Boundaries
{
  public enum RegisterUserStatus
  {
    FailureEmailRequired,
    FailurePasswordRequirementsNotMet,
    Success
  }
}