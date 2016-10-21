using System.Threading.Tasks;
using Money.Domain.Identity.Boundaries;
using Money.Domain.Identity.Entities;
using Money.Domain.Identity.Interactors;
using Moq;
using Xunit;

namespace Money.Tests.Identity
{
  public class RegisterUserTests
  {
    [Fact]
    public async Task RegistrationFailsWhenEmailIsMissing()
    {
      var request = new RegisterUserRequest { Email = "", Password = "P@ssword!!" };
      var d = new Dependencies();
      var registerUser = GetInteractor(d);

      var response = await registerUser.ExecuteAsync(request);

      Assert.Equal(RegisterUserStatus.FailureEmailRequired, response.Status);
    }

    [Fact]
    public async Task RegistrationFailsWhenPasswordIsLessThan8() 
    {
      var request = new RegisterUserRequest { Email = "a@b.c", Password = "x" };
      var d = new Dependencies();
      var registerUser = GetInteractor(d);

      var response = await registerUser.ExecuteAsync(request);

      Assert.Equal(RegisterUserStatus.FailurePasswordRequirementsNotMet, response.Status);
    }

    [Fact]
    public async Task RegisterSavesNewUserRecord()
    {
      var request = new RegisterUserRequest { Email = "a@b.c", Password = "P@ssword!!" };

      // Set the user ID when the repository is called
      var d = new Dependencies();
      d.UserRepository.Setup(x => x.AddAsync(It.IsAny<User>()))
        .Callback<User>((u) => u.Id = 99)
        .Returns(Task.CompletedTask);
      
      var registerUser = GetInteractor(d);

      var response = await registerUser.ExecuteAsync(request);

      Assert.Equal(RegisterUserStatus.Success, response.Status);
      Assert.Equal(99, response.UserId.GetValueOrDefault());

      d.UserRepository.Verify(x => x.AddAsync(It.Is<User>(u => 
        u.Email == "a@b.c" &&
        u.Password == "P@ssword!!")));
    }

    [Fact]
    public async Task RegisterSendsConfirmationEmail()
    {
      var request = new RegisterUserRequest { Email = "a@b.c", Password = "P@ssword!!" };
      var d = new Dependencies();
      var registerUser = GetInteractor(d);

      var response = await registerUser.ExecuteAsync(request);

      d.EmailService.Verify(x => x.SendAsync(It.Is<EmailMessage>(m =>
        m.To[0] == "a@b.c" &&
        m.From == "?" &&
        m.Subject == "?" &&
        m.Body == "?")));
    }

    private static IRegisterUser GetInteractor(Dependencies d)
    {
      return new RegisterUser(d.UserRepository.Object);
    }

    private class Dependencies
    {
      public Mock<IUserRepository> UserRepository { get; set; }
      //public Mock<IEmailService> EmailService { get; set; }

      public Dependencies()
      {
        UserRepository = new Mock<IUserRepository>();
      }
    }
  }
}