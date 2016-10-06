using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;

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
      var msg = _mediator.Send(new GetMessageRequest());
      ViewData["Message"] = msg;
      return View();
    }
  }

  public class GetMessageRequest : IRequest<string> { }

  public class GetMessageHandler : IRequestHandler<GetMessageRequest, string>
  {
    public string Handle(GetMessageRequest request)
    {
      return "From the handler!";
    }
  }
}