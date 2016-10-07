using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Money.Accounts.Messages;
using Web.ViewModels;

namespace Web.Controllers
{
  public class AccountsController : Controller
  {
    private readonly IMediator _mediator;
    
    public AccountsController(IMediator mediator)
    {
      _mediator = mediator;
    }

    public IActionResult Index()
    {
      var response = _mediator.Send(new GetAccountIndex());

      var viewModel = response.Accounts.Select(x => new AccountIndex
      {
        AccountId = x.AccountId,
        Name = x.Name
      }).ToList();

      return View(viewModel);
    }
  }
}