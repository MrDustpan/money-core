using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Money.Boundary.Identity.RegisterUser;

namespace Web.Features.Auth
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
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
      await _registerUserHandler.HandleAsync(request);
      
      return RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
      return View();
    }
  }
}