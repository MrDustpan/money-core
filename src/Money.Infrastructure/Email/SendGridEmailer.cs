using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Money.Infrastructure.Configuration;
using Newtonsoft.Json;

namespace Money.Infrastructure.Email
{
  public class SendGridEmailer : IEmailer
  {
    private readonly IConfigurationGateway _config;

    public SendGridEmailer(IConfigurationGateway config)
    {
      _config = config;
    }

    public async Task Send(EmailMessage message)
    {
      var apiKey = await _config.GetSendGridApiKey();
      
      var content = GetMessageContent(message);

      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("https://api.sendgrid.com");
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        await client.PostAsync("v3/mail/send", content);
      }
    }

    private static HttpContent GetMessageContent(EmailMessage message)
    {
      var sendGridMessage = new SendGridMessage(message);

      var json = JsonConvert.SerializeObject(sendGridMessage);

      return new StringContent(json, Encoding.UTF8, "application/json");
    }
  }
}