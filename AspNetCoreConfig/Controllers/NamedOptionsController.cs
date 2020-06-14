using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreConfig.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNetCoreConfig.Controllers
{
    public class NamedOptionsController : Controller
    {
        private readonly SiteOption site1;
        private readonly SiteOption site2;

        public NamedOptionsController(IOptionsMonitor<SiteOption> options)
        {
            site1 = options.Get("Site1");
            site2 = options.Get("Site2");
        }

        public IActionResult Index()
        {
            ViewBag.Site1 = site1;
            ViewBag.Site2 = site2;
            return View();
        }
    }
}
