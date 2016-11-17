using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Money.Core.Identity.Boundary.Authenticate;
using Money.Core.Identity.Boundary.Register;
using Money.Web.Features.Auth.ViewModels;

namespace Money.Web.Features.Auth
{
  public class AuthController : Controller
  {
    private readonly IRegisterHandler _registerHandler;
    private readonly IConfirmAccountHandler _confirmAccountHandler;
    private readonly IAuthenticateHandler _authenticateHandler;

    public AuthController(
      IRegisterHandler registerHandler,
      IConfirmAccountHandler confirmAccountHandler,
      IAuthenticateHandler authenticateHandler)
    {
      _registerHandler = registerHandler;
      _confirmAccountHandler = confirmAccountHandler;
      _authenticateHandler = authenticateHandler;
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
      return View();
    }

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel viewModel)
    {
      var request = new RegisterRequest
      {
        Email = viewModel.Email, 
        Password = viewModel.Password,
        ConfirmPassword = viewModel.ConfirmPassword
      };

      var response = await _registerHandler.Handle(request);

      if (response.Status == RegisterStatus.Success)
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

    [AllowAnonymous, HttpPost]
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
      var request = new AuthenticateRequest
      {
        Email = viewModel.Email, 
        Password = viewModel.Password
      };

      var response = await _authenticateHandler.Handle(request);

      if (response.Status == AuthenticateStatus.Success)
      {
        var identity = new ClaimsIdentity(
          new[] { new Claim(ClaimTypes.NameIdentifier, response.UserId.ToString()) }, 
          CookieAuthenticationDefaults.AuthenticationScheme);
          
        var user = new ClaimsPrincipal(identity);
        
        await HttpContext.Authentication.SignInAsync(
          CookieAuthenticationDefaults.AuthenticationScheme, user);
        
        return RedirectToAction("Index", "Home");
      }

      viewModel.LoadResult(response.Status);
      return View(viewModel);
    }

    public async Task<IActionResult> Logout()
    {
      await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      return RedirectToAction("Login");
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