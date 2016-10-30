namespace Money.Boundary.Identity.RegisterUser
{
  public class RegisterUserRequest
  {
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
  }
}