using System.Threading.Tasks;
using Money.Boundary.Identity.RegisterUser;
using Money.Domain.Identity;
using Money.Domain.Identity.RegisterUser;
using Moq;
using Xunit;

namespace Money.Domain.Tests.Identity.RegisterUser
{
  public class RegisterUserHandlerTests
  {
    [Fact]
    public async Task RegistrationFailsWhenEmailIsMissing()
    {
      var d = new Dependencies();
      d.Request.Email = "";

      var response = await d.Handler.HandleAsync(d.Request);

      Assert.Equal(RegisterUserStatus.FailureEmailRequired, response.Status);
    }

    [Fact]
    public async Task RegistrationFailsWhenPasswordIsLessThan8() 
    {
      var d = new Dependencies();
      d.Request.Password = null;

      var response = await d.Handler.HandleAsync(d.Request);

      Assert.Equal(RegisterUserStatus.FailurePasswordRequirementsNotMet, response.Status);
    }

    [Fact]
    public async Task RegistrationFailsWhenPasswordDoesNotMatchConfirm() 
    {
      var d = new Dependencies();
      d.Request.ConfirmPassword = "sdfsdfsdf";

      var response = await d.Handler.HandleAsync(d.Request);

      Assert.Equal(RegisterUserStatus.FailurePasswordAndConfirmDoNotMatch, response.Status);
    }

    [Fact]
    public async Task RegistrationFailsWhenAccountAlreadyExistsForEmail() 
    {
      var d = new Dependencies();
      d.UserRepository.Setup(x => x.GetUserByEmailAsync("a@b.c")).Returns(Task.FromResult(new User()));

      var response = await d.Handler.HandleAsync(d.Request);

      Assert.Equal(RegisterUserStatus.FailureEmailAlreadyExists, response.Status);
    }

    [Fact]
    public async Task SavesNewUserRecord()
    {
      // Set the user ID when the repository is called
      var d = new Dependencies();
      d.UserRepository.Setup(x => x.AddAsync(It.IsAny<User>()))
        .Callback<User>((u) => u.Id = 99)
        .Returns(Task.CompletedTask);

      var response = await d.Handler.HandleAsync(d.Request);

      Assert.Equal(RegisterUserStatus.Success, response.Status);
      Assert.Equal(99, response.UserId.GetValueOrDefault());

      d.UserRepository.Verify(x => x.AddAsync(It.Is<User>(u => 
        u.Email == "a@b.c" &&
        u.Password == "--hashed--" &&
        u.Status == UserStatus.Pending &&
        string.IsNullOrWhiteSpace(u.ConfirmationId) == false)));
    }

    [Fact]
    public async Task SendsConfirmationEmail()
    {
      var d = new Dependencies();

      var response = await d.Handler.HandleAsync(d.Request);

      d.Emailer.Verify(x => x.SendAsync(It.Is<User>(u => u.Email == "a@b.c")));
    }

    private class Dependencies
    {
      public Mock<IUserRepository> UserRepository { get; set; }
      public Mock<IConfirmationEmailSender> Emailer { get; set; }
      public Mock<IPasswordHasher> Hasher { get; set; }
      public RegisterUserRequest Request { get; set; }
      public IRegisterUserHandler Handler { get; set; }

      public Dependencies()
      {
        UserRepository = new Mock<IUserRepository>();
        Emailer = new Mock<IConfirmationEmailSender>();
        Hasher = new Mock<IPasswordHasher>();
        Hasher.Setup(x => x.Hash(It.IsAny<string>())).Returns("--hashed--");

        Handler = new RegisterUserHandler(
          UserRepository.Object, 
          Emailer.Object,
          Hasher.Object);

        Request = new RegisterUserRequest
        {
          Email = "a@b.c", 
          Password = "P@ssword!!",
          ConfirmPassword = "P@ssword!!"
        };
      }
    }
  }
}