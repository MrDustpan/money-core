using System.Threading.Tasks;

namespace Money.Boundary.Identity.Register
{
  public interface IRegisterHandler
  {
    Task<RegisterResponse> Handle(RegisterRequest request);
  }
}