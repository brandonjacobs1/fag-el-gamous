using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using fag_el_gamous.Models;
using fag_el_gamous.Data;

namespace fag_el_gamous.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MummyContext _context;
    public HomeController(ILogger<HomeController> logger, MummyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var mummies = _context.Mummies.ToList();
        return View(mummies);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

