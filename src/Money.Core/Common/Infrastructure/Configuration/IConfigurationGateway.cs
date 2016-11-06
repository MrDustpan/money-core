using System.Threading.Tasks;

namespace Money.Core.Common.Infrastructure.Configuration
{
  public interface IConfigurationGateway
  {
    Task<string> GetRegisterEmailFrom();
    Task<string> GetAccountConfirmationUrl();
    Task<string> GetSendGridApiKey();
    Task<string> GetConnectionString();
  }
}