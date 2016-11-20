using System.Threading.Tasks;

namespace Money.Core.Common.Infrastructure.Resources
{
  public class ResourceGateway : IResourceGateway
  {
    public Task<string> GetRegisterSubject()
    {
      return Task.FromResult("Please confirm your email");
    }

    public Task<string> GetRegisterBody()
    {
      return Task.FromResult("Click here to <a href=\"{0}\">confirm your account.</a>");
    }
  }
}