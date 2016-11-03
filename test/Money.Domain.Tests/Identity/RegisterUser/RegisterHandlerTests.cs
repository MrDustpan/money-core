using System.Threading.Tasks;
using Money.Boundary.Identity.Register;
using Money.Domain.Identity;
using Money.Domain.Identity.Register;
using Moq;
using Xunit;

namespace Money.Domain.Tests.Identity.Register
{
  public class RegisterHandlerTests
  {
    [Fact]
    public async Task RegistrationFailsWhenEmailIsMissing()
    {
      var d = new Dependencies();
      d.Request.Email = "";

      var response = await d.Handler.Handle(d.Request);

      Assert.Equal(RegisterStatus.FailureEmailRequired, response.Status);
    }

    [Fact]
    public async Task RegistrationFailsWhenPasswordIsLessThan8() 
    {
      var d = new Dependencies();
      d.Request.Password = null;

      var response = await d.Handler.Handle(d.Request);

      Assert.Equal(RegisterStatus.FailurePasswordRequirementsNotMet, response.Status);
    }

    [Fact]
    public async Task RegistrationFailsWhenPasswordDoesNotMatchConfirm() 
    {
      var d = new Dependencies();
      d.Request.ConfirmPassword = "sdfsdfsdf";

      var response = await d.Handler.Handle(d.Request);

      Assert.Equal(RegisterStatus.FailurePasswordAndConfirmDoNotMatch, response.Status);
    }

    [Fact]
    public async Task RegistrationFailsWhenAccountAlreadyExistsForEmail() 
    {
      var d = new Dependencies();
      d.UserRepository.Setup(x => x.GetUserByEmail("a@b.c")).Returns(Task.FromResult(new User()));

      var response = await d.Handler.Handle(d.Request);

      Assert.Equal(RegisterStatus.FailureEmailAlreadyExists, response.Status);
    }

    [Fact]
    public async Task SavesNewUserRecord()
    {
      // Set the user ID when the repository is called
      var d = new Dependencies();
      d.UserRepository.Setup(x => x.Add(It.IsAny<User>()))
        .Callback<User>((u) => u.Id = 99)
        .Returns(Task.CompletedTask);

      var response = await d.Handler.Handle(d.Request);

      Assert.Equal(RegisterStatus.Success, response.Status);
      Assert.Equal(99, response.UserId.GetValueOrDefault());

      d.UserRepository.Verify(x => x.Add(It.Is<User>(u => 
        u.Email == "a@b.c" &&
        u.Password == "--hashed--" &&
        u.Status == UserStatus.Pending &&
        string.IsNullOrWhiteSpace(u.ConfirmationId) == false)));
    }

    [Fact]
    public async Task SendsConfirmationEmail()
    {
      var d = new Dependencies();

      var response = await d.Handler.Handle(d.Request);

      d.Emailer.Verify(x => x.Send(It.Is<User>(u => u.Email == "a@b.c")));
    }

    private class Dependencies
    {
      public Mock<IUserRepository> UserRepository { get; set; }
      public Mock<IConfirmationEmailSender> Emailer { get; set; }
      public Mock<IPasswordHasher> Hasher { get; set; }
      public RegisterRequest Request { get; set; }
      public IRegisterHandler Handler { get; set; }

      public Dependencies()
      {
        UserRepository = new Mock<IUserRepository>();
        Emailer = new Mock<IConfirmationEmailSender>();
        Hasher = new Mock<IPasswordHasher>();
        Hasher.Setup(x => x.Hash(It.IsAny<string>())).Returns("--hashed--");

        Handler = new RegisterHandler(
          UserRepository.Object, 
          Emailer.Object,
          Hasher.Object);

        Request = new RegisterRequest
        {
          Email = "a@b.c", 
          Password = "P@ssword!!",
          ConfirmPassword = "P@ssword!!"
        };
      }
    }
  }
}