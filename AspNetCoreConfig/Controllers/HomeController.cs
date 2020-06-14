using AspNetCoreConfig.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace AspNetCoreConfig.Controllers
{
    class AboutInfo
    {
        public string Author { get; set; }
        public string Version { get; set; }
    }

    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            ViewBag.Value1 = _config["MyStuff:MyValues:Value1"];

            var myValues = _config.GetSection("MyStuff:MyValues");
            ViewBag.Value2 = myValues.GetValue<int>("Value2");

            ViewBag.Value3 = _config.GetValue<bool>("MyStuff:MyValues:Value3");

            AboutInfo info = new AboutInfo();
            _config.Bind("MyStuff:MyValues:About", info);
            ViewBag.Author = info.Author;
            ViewBag.Version = info.Version;

            return View();
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
}
