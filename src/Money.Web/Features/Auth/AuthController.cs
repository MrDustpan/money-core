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
    private readonly IConfirmAccountHandler _confirmAccountHandler;

    public AuthController(
      IRegisterUserHandler registerUserHandler,
      IConfirmAccountHandler confirmAccountHandler)
    {
      _registerUserHandler = registerUserHandler;
      _confirmAccountHandler = confirmAccountHandler;
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

      var response = await _registerUserHandler.Handle(request);

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
    public async Task<IActionResult> Confirm(string id)
    {
      if (string.IsNullOrWhiteSpace(id))
      {
        return View(new ConfirmViewModel());
      }

      var request = new ConfirmAccountRequest { Id = id };
      var response = await _confirmAccountHandler.Handle(request);
      
      return View(new ConfirmViewModel(response.Status));
    }
  }
}