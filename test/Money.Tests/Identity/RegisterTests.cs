using System.Threading.Tasks;
using Money.Identity;
using Money.Identity.Register;
using Money.Infrastructure;
using Moq;
using Xunit;

namespace Money.Tests.Identity
{
  public class RegisterTests
  {
    [Fact]
    public async Task RegistrationFailsWhenEmailIsMissing()
    {
      var request = new RegisterRequest { Email = "", Password = "P@ssword!!" };
      var d = new Dependencies();
      var handler = GetHandler(d);

      var response = await handler.HandleAsync(request);

      Assert.False(response.Success);
    }

    [Fact]
    public async Task RegistrationFailsWhenPasswordIsLessThan8() 
    {
      var request = new RegisterRequest { Email = "a@b.c", Password = "x" };
      var d = new Dependencies();
      var handler = GetHandler(d);

      var response = await handler.HandleAsync(request);

      Assert.False(response.Success);
    }

    [Fact]
    public async Task RegisterSavesNewUserRecord()
    {
      var request = new RegisterRequest { Email = "a@b.c", Password = "P@ssword!!" };
      var d = new Dependencies();
      var handler = GetHandler(d);

      var response = await handler.HandleAsync(request);

      Assert.True(response.Success);

      d.UserRepository.Verify(x => x.AddAsync(It.Is<User>(u => 
        u.Email == "a@b.c" &&
        u.Password == "P@ssword!!")));
    }

    [Fact]
    public async Task RegisterSendsConfirmationEmail()
    {
      var request = new RegisterRequest { Email = "a@b.c", Password = "P@ssword!!" };
      var d = new Dependencies();
      var handler = GetHandler(d);

      var response = await handler.HandleAsync(request);

      d.EmailService.Verify(x => x.SendAsync(It.Is<EmailMessage>(m =>
        m.To[0] == "a@b.c" &&
        m.From == "?" &&
        m.Subject == "?" &&
        m.Body == "?")));
    }

    private static RegisterHandler GetHandler(Dependencies d)
    {
      return new RegisterHandler(d.UserRepository.Object);
    }

    private class Dependencies
    {
      public Mock<IRepository<User>> UserRepository { get; set; }
      public Mock<IEmailService> EmailService { get; set; }

      public Dependencies()
      {
        UserRepository = new Mock<IRepository<User>>();
      }
    }
  }
}