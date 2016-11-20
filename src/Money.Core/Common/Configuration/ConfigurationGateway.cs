using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Money.Core.Common.Configuration
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

    public Task<string> GetRegisterEmailFrom()
    {
      return Task.FromResult(_config["RegisterEmailFrom"]);
    }

    public Task<string> GetAccountConfirmationUrl()
    {
      return Task.FromResult(_config["AccountConfirmationUrl"]);
    }

    public Task<string> GetSendGridApiKey()
    {
      return Task.FromResult(_config["SendGridApiKey"]);
    }

    public Task<string> GetConnectionString()
    {
      return Task.FromResult(_config.GetConnectionString("DefaultConnection"));
    }
  }
}