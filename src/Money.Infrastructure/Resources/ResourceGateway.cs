using System.Threading.Tasks;

namespace Money.Infrastructure.Resources
{
  public class ResourceGateway : IResourceGateway
  {
    public Task<string> GetRegisterUserSubject()
    {
      return Task.FromResult("Please confirm your email");
    }

    public Task<string> GetRegisterUserBody()
    {
      return Task.FromResult("Click here to <a href=\"{0}\">confirm your account.</a>");
    }
  }
}