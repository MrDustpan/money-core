using System.Threading.Tasks;

namespace Money.Infrastructure.Resources
{
  public interface IResourceGateway
  {
    Task<string> GetRegisterUserSubjectAsync();
    Task<string> GetRegisterUserBodyAsync();
  }
}