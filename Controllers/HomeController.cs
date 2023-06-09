﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using fag_el_gamous.Models;
using fag_el_gamous.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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

    // GET the Index page (landing page)
    [AllowAnonymous]
    public IActionResult Index()
    {
        

        return View();
    }

    // GET the Privacy Page
    [AllowAnonymous]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    // GET the cookie policy page
    [AllowAnonymous]
    public IActionResult CookiePolicy()
    {
        return View();
    }
}

