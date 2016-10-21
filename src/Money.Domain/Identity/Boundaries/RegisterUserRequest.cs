namespace Money.Domain.Identity.Boundaries
{
  public class RegisterUserRequest
  {
    public string Email { get; set; }
    public string Password { get; set; }
  }
}