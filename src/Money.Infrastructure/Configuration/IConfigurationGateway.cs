using System.Threading.Tasks;

namespace Money.Infrastructure.Configuration
{
  public interface IConfigurationGateway
  {
    Task<string> GetRegisterUserEmailFrom();
    Task<string> GetAccountConfirmationUrl();
    Task<string> GetSendGridApiKey();
    Task<string> GetConnectionString();
  }
}