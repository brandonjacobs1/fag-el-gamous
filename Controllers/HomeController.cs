﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using fag_el_gamous.Models;
using fag_el_gamous.Data;
using Microsoft.AspNetCore.Authorization;

namespace fag_el_gamous.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private postgresContext _context;
    public HomeController(ILogger<HomeController> logger, postgresContext context)
    {
        _logger = logger;
        _context = context;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        var test = _context.Teammembers.ToList();
        return View(test);
    }

    [Authorize]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [AllowAnonymous]
    public IActionResult CookiePolicy()
    {
        return View();
    }
}

