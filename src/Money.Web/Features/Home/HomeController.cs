﻿using Microsoft.AspNetCore.Mvc;

namespace Money.Web.Features.Home
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Error()
    {
      return View();
    }
  }
}