using Money.Boundary.Common.Email;
using Money.Infrastructure.Email;
using Xunit;

namespace Money.Infrastructure.Tests.Email
{
  public class SendGridMessageTests
  {
    [Fact]
    public void ToAddressGetsMapped()
    {
      var msg = new EmailMessage { To = "to@test.com" };

      var sendGridMsg = new SendGridMessage(msg);

      Assert.Equal("to@test.com", sendGridMsg.Personalizations[0].To[0].Email);
    }

    [Fact]
    public void SubjectGetsMapped()
    {
      var msg = new EmailMessage { Subject = "subject" };

      var sendGridMsg = new SendGridMessage(msg);

      Assert.Equal("subject", sendGridMsg.Personalizations[0].Subject);
    }

    [Fact]
    public void FromGetsMapped()
    {
      var msg = new EmailMessage { From = "from@test.com" };

      var sendGridMsg = new SendGridMessage(msg);

      Assert.Equal("from@test.com", sendGridMsg.From.Email);
    }

    [Fact]
    public void BodyGetsMapped()
    {
      var msg = new EmailMessage { Body = "Body" };

      var sendGridMsg = new SendGridMessage(msg);

      Assert.Equal("text/html", sendGridMsg.Content[0].Type);
      Assert.Equal("Body", sendGridMsg.Content[0].Value);
    }
  }
}