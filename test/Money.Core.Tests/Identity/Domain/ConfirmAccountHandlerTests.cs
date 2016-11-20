using System.Threading.Tasks;
using Money.Core.Identity.Boundary;
using Money.Core.Identity.Domain;
using Moq;
using Xunit;

namespace Money.Domain.Tests.Identity
{
  public class ConfirmAccountTests
  {
    [Fact]
    public async Task ReturnsErrorWhenNoUserMatchesConfirmationId()
    {
      var d = new Dependencies();

      d.UserRepository.Setup(x => 
        x.GetUserByConfirmationId("abc123")).Returns(Task.FromResult((User)null));

      var response = await d.Execute();

      Assert.Equal(ConfirmAccountStatus.FailureConfirmationIdNotFound, response.Status);
    }

    [Fact]
    public async Task ReturnsErrorWhenAccountIsAlreadyConfirmed()
    {
      var user = new User { Status = UserStatus.Confirmed };

      var d = new Dependencies();

      d.UserRepository.Setup(x => 
        x.GetUserByConfirmationId("abc123")).Returns(Task.FromResult(user));

      var response = await d.Execute();

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
        x.GetUserByConfirmationId("abc123")).Returns(Task.FromResult(user));

      var response = await d.Execute();

      Assert.Equal(ConfirmAccountStatus.Success, response.Status);
      Assert.Equal(UserStatus.Confirmed, user.Status);
      Assert.Null(user.ConfirmationId);
      d.UserRepository.Verify(x => x.Update(user));
    }

    private class Dependencies
    {
      public Mock<IUserRepository> UserRepository { get; set; }
      public ConfirmAccountRequest Request { get; set; }
      public IConfirmAccount ConfirmAccount { get; set; }
      
      public Dependencies()
      {
        UserRepository = new Mock<IUserRepository>();
        Request = new ConfirmAccountRequest { Id = "abc123" };
        ConfirmAccount = new ConfirmAccount(UserRepository.Object);
      }

      public async Task<ConfirmAccountResponse> Execute()
      {
        return await ConfirmAccount.Execute(Request);
      }
    }
  }
}