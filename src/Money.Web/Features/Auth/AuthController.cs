using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Money.Boundary.Identity.RegisterUser;
using Money.Web.Features.Auth.ViewModels;

namespace Money.Web.Features.Auth
{
  public class AuthController : Controller
  {
    private readonly IRegisterUserHandler _registerUserHandler;

    public AuthController(IRegisterUserHandler registerUserHandler)
    {
      _registerUserHandler = registerUserHandler;
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
      return View();
    }

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
      var request = new RegisterUserRequest
      {
        Email = viewModel.Email, 
        Password = viewModel.Password,
        ConfirmPassword = viewModel.ConfirmPassword
      };

      var response = await _registerUserHandler.HandleAsync(request);

      if (response.Status == RegisterUserStatus.Success)
      {
        return RedirectToAction("Confirm", "Auth");
      }

      viewModel.LoadResult(response.Status);
      return View(viewModel);
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
      return View();
    }

    [AllowAnonymous]
    public IActionResult Confirm(string id)
    {
      return View();
    }
  }
}