using System.Threading.Tasks;

namespace Money.Infrastructure.Resources
{
  public class ResourceGateway : IResourceGateway
  {
    public Task<string> GetRegisterUserSubjectAsync()
    {
      return Task.FromResult("Please confirm your email");
    }

    public Task<string> GetRegisterUserBodyAsync()
    {
      return Task.FromResult("Click here to <a href=\"{0}\">confirm your account.</a>");
    }
  }
}