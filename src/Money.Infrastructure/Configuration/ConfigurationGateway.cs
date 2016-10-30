using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Money.Boundary.Common.Configuration;

namespace Money.Infrastructure.Configuration
{
  public class ConfigurationGateway : IConfigurationGateway
  {
    private readonly IConfigurationRoot _config;

    public ConfigurationGateway()
    {
      var builder = new ConfigurationBuilder();
      builder.SetBasePath(Directory.GetCurrentDirectory());
      builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
      builder.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);
      _config = builder.Build();
    }

    public Task<string> GetRegisterUserEmailFromAsync()
    {
      return Task.FromResult(_config["RegisterUserEmailFrom"]);
    }

    public Task<string> GetAccountConfirmationUrlAsync()
    {
      return Task.FromResult(_config["AccountConfirmationUrl"]);
    }

    public Task<string> GetSendGridApiKeyAsync()
    {
      return Task.FromResult(_config["SendGridApiKey"]);
    }
  }
}