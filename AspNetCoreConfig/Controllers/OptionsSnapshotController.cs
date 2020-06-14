using AspNetCoreConfig.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNetCoreConfig.Controllers
{
    public class OptionsSnapshotController : Controller
    {
        private readonly IOptionsSnapshot<MyValuesOptions> _options;

        public OptionsSnapshotController(IOptionsSnapshot<MyValuesOptions> options)
        {
           _options = options;
        }

        public IActionResult Index()
        {
            return View(_options.Value);
        }
    }
}
