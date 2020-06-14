using AspNetCoreConfig.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetCoreConfig.Controllers
{
    public class OptionsMonitorController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptionsMonitor<MyValuesOptions> _options;

        public OptionsMonitorController(IOptionsMonitor<MyValuesOptions> options, ILogger<HomeController> logger)
        {
            _logger = logger;
            _options = options;

            _options.OnChange(config =>
            {
                _logger.LogInformation("OptionMonitor.OnChange was fired");
                _logger.LogInformation($"Value1={config.Value1}, Value2={config.Value2}, Value3={config.Value3}");
            });
        }

        public IActionResult Index()
        {
            return View(_options.CurrentValue);
        }
    }
}
