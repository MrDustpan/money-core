namespace Money.Core.Identity.Boundary.Authenticate
{
  public class AuthenticateRequest
  {
    public string Email { get; set; }
    public string Password { get; set; }
  }
}