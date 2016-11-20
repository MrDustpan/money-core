using System.Threading.Tasks;

namespace Money.Core.Accounts.Boundary
{
  public interface ICreateAccount
  {
    Task<CreateAccountResponse> Execute(CreateAccountRequest request);
  }

  public class CreateAccountRequest
  {
    public int UserId { get; set; }
    public string Name { get; set; }
    public decimal CurrentBalance { get; set; }
  }

  public class CreateAccountResponse
  {
    public int AccountId { get; set; }
  }
}