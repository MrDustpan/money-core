using System.Threading.Tasks;

namespace Money.Core.Common.Resources
{
  public interface IResourceGateway
  {
    Task<string> GetRegisterSubject();
    Task<string> GetRegisterBody();
  }
}