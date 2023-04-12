using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fag_el_gamous.Controllers
{
    public class ResearchController : Controller
    {
        //[Authorize]
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Research()
        {
            return View();
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Research(string request)
        {
            return RedirectToAction("Index", request);
        }
    }
}

