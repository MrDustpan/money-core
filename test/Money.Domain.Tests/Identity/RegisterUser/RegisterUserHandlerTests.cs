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
      var request = new RegisterUserRequest { Email = "", Password = "P@ssword!!" };
      var d = new Dependencies();
      var handler = GetHandler(d);

      var response = await handler.HandleAsync(request);

      Assert.Equal(RegisterUserStatus.FailureEmailRequired, response.Status);
    }

    [Fact]
    public async Task RegistrationFailsWhenPasswordIsLessThan8() 
    {
      var request = new RegisterUserRequest { Email = "a@b.c", Password = "x" };
      var d = new Dependencies();
      var handler = GetHandler(d);

      var response = await handler.HandleAsync(request);

      Assert.Equal(RegisterUserStatus.FailurePasswordRequirementsNotMet, response.Status);
    }

    [Fact]
    public async Task SavesNewUserRecord()
    {
      var request = new RegisterUserRequest { Email = "a@b.c", Password = "P@ssword!!" };

      // Set the user ID when the repository is called
      var d = new Dependencies();
      d.UserRepository.Setup(x => x.AddAsync(It.IsAny<User>()))
        .Callback<User>((u) => u.Id = 99)
        .Returns(Task.CompletedTask);
      
      var handler = GetHandler(d);

      var response = await handler.HandleAsync(request);

      Assert.Equal(RegisterUserStatus.Success, response.Status);
      Assert.Equal(99, response.UserId.GetValueOrDefault());

      d.UserRepository.Verify(x => x.AddAsync(It.Is<User>(u => 
        u.Email == "a@b.c" &&
        u.Password == "P@ssword!!" &&
        string.IsNullOrWhiteSpace(u.ConfirmationId) == false)));
    }

    [Fact]
    public async Task SendsConfirmationEmail()
    {
      var request = new RegisterUserRequest { Email = "a@b.c", Password = "P@ssword!!" };
      var d = new Dependencies();
      var handler = GetHandler(d);

      var response = await handler.HandleAsync(request);

      d.Emailer.Verify(x => x.SendAsync(It.Is<User>(u => u.Email == "a@b.c")));
    }

    private static IRegisterUserHandler GetHandler(Dependencies d)
    {
      return new RegisterUserHandler(d.UserRepository.Object, d.Emailer.Object);
    }

    private class Dependencies
    {
      public Mock<IUserRepository> UserRepository { get; set; }
      public Mock<IConfirmationEmailSender> Emailer { get; set; }

      public Dependencies()
      {
        UserRepository = new Mock<IUserRepository>();
        Emailer = new Mock<IConfirmationEmailSender>();
      }
    }
  }
}