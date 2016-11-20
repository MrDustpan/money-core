using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Money.Core.Accounts.Boundary.CreateAccount;
using Money.Core.Accounts.Boundary.GetAccountIndex;
using Money.Web.Features.Accounts.ViewModels;
using Money.Web.Features.Shared;

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
      var request = new GetAccountIndexRequest { UserId = User.GetId() };
      var index = await _getAccountIndexHandler.Handle(request);

      if (index.Accounts.Any())
      {
        return RedirectToAction("Transactions", new { id = index.Accounts.First().Id });
      }

      return View();
    }

    public async Task<IActionResult> Transactions(int id)
    {
      var request = new GetAccountIndexRequest { UserId = User.GetId() };
      var index = await _getAccountIndexHandler.Handle(request);
      var viewModel = new AccountTransactionsViewModel(id, index);

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
        UserId = User.GetId(),
        Name = viewModel.Name,
        CurrentBalance = viewModel.CurrentBalance
      };
      
      await _createAccountHandler.Handle(request);

      return RedirectToAction("Index");
    }
  }
}