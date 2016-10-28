using System.Threading.Tasks;

namespace Money.Boundary.Common.Resources
{
  public interface IResourceGateway
  {
    Task<string> GetRegisterUserSubjectAsync();
    Task<string> GetRegisterUserBodyAsync();
  }
}