using System.Threading.Tasks;
using Money.Web.Features.Auth;
using Money.Boundary.Identity.RegisterUser;
using Moq;
using Xunit;
using Money.Web.Features.Auth.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Money.Web.Tests.Auth
{
  public class AuthControllerTests
  {
    [Fact]
    public async Task RegisterReturnsViewOnError()
    {
      var d = new Dependencies();
      d.Handler.Setup(x => x.HandleAsync(It.IsAny<RegisterUserRequest>()))
        .Returns(Task.FromResult(new RegisterUserResponse { Status = RegisterUserStatus.FailureEmailRequired }));

      var result = (await d.Controller.Register(new RegisterViewModel())) as ViewResult;
      var resultView = result.Model as RegisterViewModel;

      Assert.NotNull(resultView);
    }

    [Fact]
    public async Task RegisterRedirectsToConfirmOnSuccess()
    {
      var d = new Dependencies();
      d.Handler.Setup(x => x.HandleAsync(It.IsAny<RegisterUserRequest>()))
        .Returns(Task.FromResult(new RegisterUserResponse { Status = RegisterUserStatus.Success }));

      var result = (await d.Controller.Register(new RegisterViewModel())) as RedirectToActionResult;

      Assert.Equal("Auth", result.ControllerName);
      Assert.Equal("Confirm", result.ActionName);
    }

    private class Dependencies
    {
      public Mock<IRegisterUserHandler> Handler { get; set; }
      public AuthController Controller { get; set; }

      public Dependencies()
      {
        Handler = new Mock<IRegisterUserHandler>();
        Controller = new AuthController(Handler.Object);
      }
    }
  }
}