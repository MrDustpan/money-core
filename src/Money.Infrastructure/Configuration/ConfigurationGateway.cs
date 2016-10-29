using System.Threading.Tasks;
using Money.Boundary.Common.Configuration;

namespace Money.Infrastructure.Configuration
{
  public class ConfigurationGateway : IConfigurationGateway
  {
    public Task<string> GetRegisterUserEmailFromAsync()
    {
      return Task.FromResult("test@azure.com");
    }

    public Task<string> GetAccountConfirmationUrlAsync()
    {
      return Task.FromResult("/auth/confirm?id={0}");
    }
  }
}