using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Money.Boundary.Common.Email;
using Newtonsoft.Json;

namespace Money.Infrastructure.Email
{
  public class SendGridEmailer : IEmailer
  {
    public async Task SendAsync(EmailMessage message)
    {
      const string apiKey = "--redacted--";
      
      var sendGridMessage = new SendGridMessage(message);

      var json = JsonConvert.SerializeObject(sendGridMessage);
      var content = new StringContent(json, Encoding.UTF8, "application/json");

      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri("https://api.sendgrid.com");
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        await client.PostAsync("v3/mail/send", content);
      }
    }
  }
}