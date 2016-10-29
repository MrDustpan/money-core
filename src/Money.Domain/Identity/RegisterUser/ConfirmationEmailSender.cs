using System.Threading.Tasks;
using Money.Boundary.Common.Configuration;
using Money.Boundary.Common.Email;
using Money.Boundary.Common.Resources;

namespace Money.Domain.Identity.RegisterUser
{
  public class ConfirmationEmailSender : IConfirmationEmailSender
  {
    private readonly IEmailer _emailer;
    private readonly IConfigurationGateway _config;
    private readonly IResourceGateway _resources;

    public ConfirmationEmailSender(IEmailer emailer, IConfigurationGateway config, IResourceGateway resources)
    {
      _emailer = emailer;
      _config = config;
      _resources = resources;
    }

    public async Task SendAsync(User user)
    {
      var message = new EmailMessage
      { 
        To = user.Email,
        From = await _config.GetRegisterUserEmailFromAsync(),
        Subject = await _resources.GetRegisterUserSubjectAsync(),
        Body = await GetBody(user.ConfirmationId)
      };

      await _emailer.SendAsync(message);
    }

    private async Task<string> GetBody(string confirmationId)
    {
      var url = await _config.GetAccountConfirmationUrlAsync();

      var fullUrl = string.IsNullOrWhiteSpace(url) ? "" : string.Format(url, confirmationId);

      var body = await _resources.GetRegisterUserBodyAsync();

      return string.IsNullOrWhiteSpace(body) ? "" : string.Format(body, fullUrl);
    }
  }

  public interface IConfirmationEmailSender
  {
    Task SendAsync(User user);
  }
}