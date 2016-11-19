using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Money.Core.Accounts.Boundary.CreateAccount;
using Money.Web.Features.Accounts.ViewModels;

namespace Money.Web.Features.Accounts
{
  public class AccountsController : Controller
  {
    private readonly ICreateAccountHandler _createAccountHandler;

    public AccountsController(ICreateAccountHandler createAccountHandler)
    {
      _createAccountHandler = createAccountHandler;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Add()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddAccountViewModel viewModel)
    {
      var request = new CreateAccountRequest
      {
        Name = viewModel.Name,
        CurrentBalance = viewModel.CurrentBalance
      };
      
      await _createAccountHandler.Handle(request);

      return RedirectToAction("Index");
    }
  }
}