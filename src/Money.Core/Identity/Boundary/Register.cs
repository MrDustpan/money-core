using System.Threading.Tasks;

namespace Money.Core.Identity.Boundary
{
  public interface IRegister
  {
    Task<RegisterResponse> Execute(RegisterRequest request);
  }

  public class RegisterRequest
  {
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
  }

  public class RegisterResponse
  {
    public RegisterStatus Status { get; set; }
    public int? UserId { get; set; }
  }

  public enum RegisterStatus
  {
    FailureEmailRequired,
    FailurePasswordRequirementsNotMet,
    FailurePasswordAndConfirmDoNotMatch,
    FailureEmailAlreadyExists,
    Success
  }
}