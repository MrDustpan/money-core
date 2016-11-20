using System.Threading.Tasks;
using Money.Core.Common.Configuration;
using Money.Core.Common.Email;
using Money.Core.Common.Resources;
using Money.Core.Identity.Domain;

namespace Money.Core.Identity.Infrastructure
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

    public async Task Send(User user)
    {
      var message = new EmailMessage
      { 
        To = user.Email,
        From = await _config.GetRegisterEmailFrom(),
        Subject = await _resources.GetRegisterSubject(),
        Body = await GetBody(user.ConfirmationId)
      };

      await _emailer.Send(message);
    }

    private async Task<string> GetBody(string confirmationId)
    {
      var url = await _config.GetAccountConfirmationUrl();

      var fullUrl = string.IsNullOrWhiteSpace(url) ? "" : string.Format(url, confirmationId);

      var body = await _resources.GetRegisterBody();

      return string.IsNullOrWhiteSpace(body) ? "" : string.Format(body, fullUrl);
    }
  }
}