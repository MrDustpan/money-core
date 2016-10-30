using System.Threading.Tasks;

namespace Money.Boundary.Common.Configuration
{
  public interface IConfigurationGateway
  {
    Task<string> GetRegisterUserEmailFromAsync();
    Task<string> GetAccountConfirmationUrlAsync();
    Task<string> GetSendGridApiKeyAsync();
  }
}