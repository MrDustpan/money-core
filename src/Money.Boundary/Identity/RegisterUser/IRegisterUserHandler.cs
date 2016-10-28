using System.Threading.Tasks;

namespace Money.Boundary.Identity.RegisterUser
{
  public interface IRegisterUserHandler
  {
    Task<RegisterUserResponse> HandleAsync(RegisterUserRequest request);
  }
}