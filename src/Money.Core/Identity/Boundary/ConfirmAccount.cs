using System.Threading.Tasks;

namespace Money.Core.Identity.Boundary
{
  public interface IConfirmAccount
  {
    Task<ConfirmAccountResponse> Execute(ConfirmAccountRequest request);
  }

  public class ConfirmAccountRequest
  {
    public string Id { get; set; }
  }

  public class ConfirmAccountResponse
  {
    public ConfirmAccountStatus Status { get; set; }
  }

  public enum ConfirmAccountStatus
  {
    FailureConfirmationIdNotFound,
    FailureAlreadyConfirmed,
    Success
  }
}