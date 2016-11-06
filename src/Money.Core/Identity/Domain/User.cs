namespace Money.Core.Identity.Domain
{
  public class User
  {
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmationId { get; set; }
    public UserStatus Status { get; set; }
    public int FailedAttempts { get; set; }
  }

  public enum UserStatus
  {
    Pending = 1,
    Confirmed = 2,
    Locked = 3
  }
}