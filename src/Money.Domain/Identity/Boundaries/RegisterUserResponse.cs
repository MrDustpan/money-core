namespace Money.Domain.Identity.Boundaries
{
  public class RegisterUserResponse
  {
    public RegisterUserStatus Status { get; set; }
    public int? UserId { get; set; }
  }
}