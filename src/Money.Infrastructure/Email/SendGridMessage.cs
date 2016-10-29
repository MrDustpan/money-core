using System.Collections.Generic;
using Money.Boundary.Common.Email;

namespace Money.Infrastructure.Email
{
  public class SendGridMessage
  {
    public List<Personalization> Personalizations { get; set; }
    public EmailAddress From { get; set; }
    public List<EmailContent> Content { get; set; }

    public SendGridMessage(EmailMessage message)
    {
      Personalizations = new List<Personalization>
      {
        new Personalization(message.To, message.Subject)
      };

      From = new EmailAddress { Email = message.From };

      Content = new List<EmailContent>
      {
        new EmailContent { Type = "text/html", Value = message.Body }
      };
    }
  }

  public class Personalization
  {
    public List<EmailAddress> To { get; set; }
    public string Subject { get; set; }

    public Personalization(string to, string subject)
    {
      To = new List<EmailAddress>
      {
        new EmailAddress { Email = to }
      };

      Subject = subject;
    }
  }

  public class EmailAddress
  {
    public string Email { get; set; }
  }

  public class EmailContent
  {
    public string Type { get; set; }
    public string Value { get; set; }
  }
}