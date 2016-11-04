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
    public async Task AuthenticateFailsWhenAccountIsLocked()
    {
      var d = new Dependencies();
      d.User.Status = UserStatus.Locked;
      d.PasswordValidator.Setup(x => x.IsValid(d.Request.Password, d.User.Password)).Returns(false);

      var response = await d.Handler.Handle(d.Request);

      Assert.Equal(AuthenticateStatus.AccountLocked, response.Status);
    }

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

    [Fact]
    public async Task FailedAttemptsIsIncrementedWhenPasswordIsInvalid()
    {
      var d = new Dependencies();
      d.PasswordValidator.Setup(x => x.IsValid(d.Request.Password, d.User.Password)).Returns(false);

      var response = await d.Handler.Handle(d.Request);

      d.UserRepository.Verify(x => x.Update(It.Is<User>(u => u.FailedAttempts == 1)));
    }

    [Fact]
    public async Task AccountIsLockedAfterFiveFailedAttempts()
    {
      var d = new Dependencies();
      d.User.FailedAttempts = 4;
      d.PasswordValidator.Setup(x => x.IsValid(d.Request.Password, d.User.Password)).Returns(false);

      var response = await d.Handler.Handle(d.Request);

      Assert.Equal(AuthenticateStatus.AccountLocked, response.Status);

      d.UserRepository.Verify(x => x.Update(It.Is<User>(u => 
        u.FailedAttempts == 5 &&
        u.Status == UserStatus.Locked)));
    }

    [Fact]
    public async Task AuthenticateSuccess()
    {
      var d = new Dependencies();
      d.PasswordValidator.Setup(x => x.IsValid(d.Request.Password, d.User.Password)).Returns(true);

      var response = await d.Handler.Handle(d.Request);

      Assert.Equal(AuthenticateStatus.Success, response.Status);
    }

    [Fact]
    public async Task FailedAttemptsIsResetIfGreaterThanZeroWhenSuccessful()
    {
      var d = new Dependencies();
      d.User.FailedAttempts = 1;
      d.PasswordValidator.Setup(x => x.IsValid(d.Request.Password, d.User.Password)).Returns(true);

      var response = await d.Handler.Handle(d.Request);

      d.UserRepository.Verify(x => x.Update(It.Is<User>(u => 
        u.FailedAttempts == 0 &&
        u.Status == UserStatus.Confirmed)));
    }

    [Fact]
    public async Task FailedAttemptsIsNotResetIfZeroWhenSuccessful()
    {
      var d = new Dependencies();
      d.User.FailedAttempts = 0;
      d.PasswordValidator.Setup(x => x.IsValid(d.Request.Password, d.User.Password)).Returns(true);

      var response = await d.Handler.Handle(d.Request);

      d.UserRepository.Verify(x => x.Update(It.IsAny<User>()), Times.Never());
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