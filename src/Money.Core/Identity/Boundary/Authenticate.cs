using System.Threading.Tasks;

namespace Money.Core.Identity.Boundary
{
  public interface IAuthenticate
  {
    Task<AuthenticateResponse> Execute(AuthenticateRequest request);
  }

  public class AuthenticateRequest
  {
    public string Email { get; set; }
    public string Password { get; set; }
  }

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