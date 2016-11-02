using System.Threading.Tasks;
using Money.Boundary.Identity.RegisterUser;
using Money.Domain.Identity;
using Money.Domain.Identity.RegisterUser;
using Moq;
using Xunit;

namespace Money.Domain.Tests.Identity.RegisterUser
{
  public class ConfirmAccountHandlerTests
  {
    [Fact]
    public async Task ReturnsErrorWhenNoUserMatchesConfirmationId()
    {
      var d = new Dependencies();

      d.UserRepository.Setup(x => 
        x.GetUserByConfirmationIdAsync("abc123")).Returns(Task.FromResult((User)null));

      var response = await d.HandleAsync();

      Assert.Equal(ConfirmAccountStatus.FailureConfirmationIdNotFound, response.Status);
    }

    [Fact]
    public async Task ReturnsErrorWhenAccountIsAlreadyConfirmed()
    {
      var user = new User { Status = UserStatus.Confirmed };

      var d = new Dependencies();

      d.UserRepository.Setup(x => 
        x.GetUserByConfirmationIdAsync("abc123")).Returns(Task.FromResult(user));

      var response = await d.HandleAsync();

      Assert.Equal(ConfirmAccountStatus.FailureAlreadyConfirmed, response.Status);
    }

    [Fact]
    public async Task UpdatesUserToConfirmed()
    {
      var user = new User
      {
        Id = 99,
        Status = UserStatus.Pending,
        ConfirmationId = "abc123"
      };

      var d = new Dependencies();

      d.UserRepository.Setup(x => 
        x.GetUserByConfirmationIdAsync("abc123")).Returns(Task.FromResult(user));

      var response = await d.HandleAsync();

      Assert.Equal(ConfirmAccountStatus.Success, response.Status);
      Assert.Equal(UserStatus.Confirmed, user.Status);
      Assert.Null(user.ConfirmationId);
      d.UserRepository.Verify(x => x.UpdateAsync(user));
    }

    private class Dependencies
    {
      public Mock<IUserRepository> UserRepository { get; set; }
      public ConfirmAccountRequest Request { get; set; }
      public IConfirmAccountHandler Handler { get; set; }
      
      public Dependencies()
      {
        UserRepository = new Mock<IUserRepository>();
        Request = new ConfirmAccountRequest { Id = "abc123" };
        Handler = new ConfirmAccountHandler(UserRepository.Object);
      }

      public async Task<ConfirmAccountResponse> HandleAsync()
      {
        return await Handler.HandleAsync(Request);
      }
    }
  }
}