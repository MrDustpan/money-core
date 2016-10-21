using System.Threading.Tasks;

namespace Money.Domain.Identity.Boundaries
{
  public interface IRegisterUser
  {
    Task<RegisterUserResponse> ExecuteAsync(RegisterUserRequest request);
  }
}