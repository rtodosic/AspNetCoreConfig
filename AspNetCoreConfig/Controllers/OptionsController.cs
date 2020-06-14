using AspNetCoreConfig.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNetCoreConfig.Controllers
{
    public class OptionsController : Controller
    {
        private readonly IOptions<MyValuesOptions> _options;

        public OptionsController(IOptions<MyValuesOptions> options)
        {
           _options = options;
        }

        public IActionResult Index()
        {
            return View(_options.Value);
        }
    }
}
