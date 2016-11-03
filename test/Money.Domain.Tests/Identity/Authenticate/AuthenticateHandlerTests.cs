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

    [Fact]
    public async Task AuthenticateFailsWhenPasswordIsInvalid()
    {
      var d = new Dependencies();
      d.PasswordValidator.Setup(x => x.IsValid(d.Request.Password, d.User.Password)).Returns(false);

      var response = await d.Handler.Handle(d.Request);

      Assert.Equal(AuthenticateStatus.InvalidPassword, response.Status);
    }

    private class Dependencies
    {
      public AuthenticateRequest Request { get; set; }
      public User User { get; set; }
      public Mock<IUserRepository> UserRepository { get; set; }
      public Mock<IPasswordValidator> PasswordValidator { get; set; }
      public IAuthenticateHandler Handler { get; set; }

      public Dependencies()
      {
        Request = new AuthenticateRequest { Email = "a@b.c", Password = "password" };
        User = new User { Id = 1, Email = "a@b.c", Password = "--hashed--" };
        UserRepository = new Mock<IUserRepository>();
        UserRepository.Setup(x => x.GetUserByEmail("a@b.c")).Returns(Task.FromResult(User));
        PasswordValidator = new Mock<IPasswordValidator>();
        Handler = new AuthenticateHandler(UserRepository.Object, PasswordValidator.Object);
      }
    }
  }
}