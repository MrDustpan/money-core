namespace Money.Boundary.Identity.Authenticate
{
  public class AuthenticateResponse
  {
    public AuthenticateStatus Status { get; set; }
  }

  public enum AuthenticateStatus
  {
    UserNotFound,
    InvalidPassword,
    AccountLocked,
    Success
  }
}