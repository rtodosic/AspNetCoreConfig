using AspNetCoreConfig.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNetCoreConfig.Controllers
{
    public class AnnotationValidationController : Controller
    {
        private readonly IOptions<SiteValidationOptions> _options;

        public AnnotationValidationController(IOptions<SiteValidationOptions> options)
        {
            _options = options;
        }
        public IActionResult Index()
        {
            ViewBag.Site1 = _options.Value;
            return View();
        }
    }
}
