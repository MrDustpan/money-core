using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Money.Core.Accounts.Boundary.CreateAccount;
using Money.Core.Accounts.Boundary.GetAccountIndex;
using Money.Web.Features.Accounts.ViewModels;

namespace Money.Web.Features.Accounts
{
  public class AccountsController : Controller
  {
    private readonly IGetAccountIndexHandler _getAccountIndexHandler;
    private readonly ICreateAccountHandler _createAccountHandler;

    public AccountsController(IGetAccountIndexHandler getAccountIndexHandler, ICreateAccountHandler createAccountHandler)
    {
      _getAccountIndexHandler = getAccountIndexHandler;
      _createAccountHandler = createAccountHandler;
    }

    public async Task<IActionResult> Index()
    {
      var accounts = await _getAccountIndexHandler.Handle(new GetAccountIndexRequest());
      var viewModel = new AccountIndexViewModel(accounts);

      return View(viewModel);
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