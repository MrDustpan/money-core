using System.Threading.Tasks;

namespace Money.Core.Common.Infrastructure.Resources
{
  public interface IResourceGateway
  {
    Task<string> GetRegisterSubject();
    Task<string> GetRegisterBody();
  }
}