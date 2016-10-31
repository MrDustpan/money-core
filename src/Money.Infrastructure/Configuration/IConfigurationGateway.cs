using System.Threading.Tasks;

namespace Money.Infrastructure.Configuration
{
  public interface IConfigurationGateway
  {
    Task<string> GetRegisterUserEmailFromAsync();
    Task<string> GetAccountConfirmationUrlAsync();
    Task<string> GetSendGridApiKeyAsync();
    Task<string> GetConnectionStringAsync();
  }
}