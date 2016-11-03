using System.Threading.Tasks;
using Money.Boundary.Identity.Authenticate;
using Money.Domain.Identity;
using Money.Domain.Identity.Authenticate;
using Moq;
using Xunit;

namespace Money.Domain.Tests.Identity.Authenticate
{
  public class AuthenticateHandlerTests
  {
    [Fact]
    public async Task AuthenticateFailsWhenUserNotFound()
    {
      var d = new Dependencies();
      d.UserRepository.Setup(x => x.GetUserByEmail(d.Request.Email)).Returns(Task.FromResult((User)null));

      var response = await d.Handler.Handle(d.Request);

      Assert.Equal(AuthenticateStatus.UserNotFound, response.Status);
    }

    private class Dependencies
    {
      public AuthenticateRequest Request { get; set; }
      public Mock<IUserRepository> UserRepository { get; set; }
      public IAuthenticateHandler Handler { get; set; }

      public Dependencies()
      {
        Request = new AuthenticateRequest { Email = "a@b.c" };
        UserRepository = new Mock<IUserRepository>();
        Handler = new AuthenticateHandler(UserRepository.Object);
      }
    }
  }
}