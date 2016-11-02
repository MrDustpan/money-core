using System.Threading.Tasks;

namespace Money.Infrastructure.Resources
{
  public interface IResourceGateway
  {
    Task<string> GetRegisterUserSubject();
    Task<string> GetRegisterUserBody();
  }
}