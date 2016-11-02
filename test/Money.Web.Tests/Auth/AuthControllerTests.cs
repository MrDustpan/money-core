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
      d.RegisterUserHandler.Setup(x => x.Handle(It.IsAny<RegisterUserRequest>()))
        .Returns(Task.FromResult(new RegisterUserResponse { Status = RegisterUserStatus.FailureEmailRequired }));

      var result = (await d.Controller.Register(new RegisterViewModel())) as ViewResult;
      var resultView = result.Model as RegisterViewModel;

      Assert.NotNull(resultView);
    }

    [Fact]
    public async Task RegisterRedirectsToConfirmOnSuccess()
    {
      var d = new Dependencies();
      d.RegisterUserHandler.Setup(x => x.Handle(It.IsAny<RegisterUserRequest>()))
        .Returns(Task.FromResult(new RegisterUserResponse { Status = RegisterUserStatus.Success }));

      var result = (await d.Controller.Register(new RegisterViewModel())) as RedirectToActionResult;

      Assert.Equal("Auth", result.ControllerName);
      Assert.Equal("Confirm", result.ActionName);
    }

    private class Dependencies
    {
      public Mock<IRegisterUserHandler> RegisterUserHandler { get; set; }
      public Mock<IConfirmAccountHandler> ConfirmAccountHandler { get; set; }
      public AuthController Controller { get; set; }

      public Dependencies()
      {
        RegisterUserHandler = new Mock<IRegisterUserHandler>();
        ConfirmAccountHandler = new Mock<IConfirmAccountHandler>();
        
        Controller = new AuthController(
          RegisterUserHandler.Object, 
          ConfirmAccountHandler.Object);
      }
    }
  }
}