using System.Threading.Tasks;

namespace Money.Boundary.Common.Configuraion
{
  public interface IConfigurationGateway
  {
    Task<string> GetRegisterUserEmailFromAsync();
    Task<string> GetAccountConfirmationUrlAsync();
  }
}