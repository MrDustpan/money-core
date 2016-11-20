using System.Threading.Tasks;

namespace Money.Core.Identity.Boundary.Register
{
  public interface IRegisterHandler
  {
    Task<RegisterResponse> Handle(RegisterRequest request);
  }
}