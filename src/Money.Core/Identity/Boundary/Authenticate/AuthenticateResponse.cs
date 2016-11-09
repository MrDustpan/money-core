namespace Money.Core.Identity.Boundary.Authenticate
{
  public class AuthenticateResponse
  {
    public AuthenticateStatus Status { get; set; }
    public int UserId { get; set; }
  }
  
  public enum AuthenticateStatus
  {
    UserNotFound,
    InvalidPassword,
    AccountLocked,
    Success
  }
}