using System.Threading.Tasks;
using Money.Infrastructure.Configuration;
using Money.Infrastructure.Email;
using Money.Infrastructure.Resources;
using Money.Domain.Identity;
using Money.Domain.Identity.RegisterUser;
using Moq;
using Xunit;

namespace Money.Infrastructure.Tests.Email
{
  public class ConfirmationEmailSenderTests
  {
    [Fact]
    public async Task ToAddressIsSetFromUserEmail()
    {
      var d = new Dependencies();

      var emailer = GetConfirmationEmailSender(d);

      await emailer.SendAsync(d.User);

      d.Emailer.Verify(x => x.SendAsync(It.Is<EmailMessage>(e => e.To == d.User.Email)));
    }

    [Fact]
    public async Task FromAddressIsSetFromConfig()
    {
      var d = new Dependencies();
      d.Config.Setup(x => x.GetRegisterUserEmailFromAsync()).Returns(Task.FromResult("x@y.z"));

      var emailer = GetConfirmationEmailSender(d);

      await emailer.SendAsync(d.User);

      d.Emailer.Verify(x => x.SendAsync(It.Is<EmailMessage>(e => e.From == "x@y.z")));
    }

    [Fact]
    public async Task SubjectIsSetFromResource()
    {
      var d = new Dependencies();
      d.Resources.Setup(x => x.GetRegisterUserSubjectAsync()).Returns(Task.FromResult("Confirm your account"));

      var emailer = GetConfirmationEmailSender(d);

      await emailer.SendAsync(d.User);

      d.Emailer.Verify(x => x.SendAsync(It.Is<EmailMessage>(e => e.Subject == "Confirm your account")));
    }

    [Fact]
    public async Task BodyIsSetFromResource()
    {
      var d = new Dependencies();

      d.Config.Setup(x => x.GetAccountConfirmationUrlAsync()).Returns(Task.FromResult("confirm?id={0}"));

      d.User.ConfirmationId = "abc123";

      d.Resources
        .Setup(x => x.GetRegisterUserBodyAsync())
        .Returns(Task.FromResult("Click here to <a href=\"{0}\">confirm your account.</a>"));

      const string expected = "Click here to <a href=\"confirm?id=abc123\">confirm your account.</a>";

      var emailer = GetConfirmationEmailSender(d);

      await emailer.SendAsync(d.User);

      d.Emailer.Verify(x => x.SendAsync(It.Is<EmailMessage>(e => e.Body == expected)));
    }

    private static IConfirmationEmailSender GetConfirmationEmailSender(Dependencies d)
    {
      return new ConfirmationEmailSender(
        d.Emailer.Object, 
        d.Config.Object,
        d.Resources.Object);
    }

    private class Dependencies
    {
      public User User { get; set; }
      public Mock<IEmailer> Emailer { get; set; }
      public Mock<IConfigurationGateway> Config { get; set; }
      public Mock<IResourceGateway> Resources { get; set; }

      public Dependencies()
      {
        User = new User { Email = "a@b.c" };
        Emailer = new Mock<IEmailer>();
        Config = new Mock<IConfigurationGateway>();
        Resources = new Mock<IResourceGateway>();
      }
    }
  }
}