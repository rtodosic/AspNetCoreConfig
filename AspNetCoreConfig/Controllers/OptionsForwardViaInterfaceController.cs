using AspNetCoreConfig.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreConfig.Controllers
{
    public class OptionsForwardViaInterfaceController : Controller
    {
        private readonly IMyValuesConfiguration _myValues;

        public OptionsForwardViaInterfaceController(IMyValuesConfiguration myValues)
        {
            _myValues = myValues;
        }
        public IActionResult Index()
        {
            return View(_myValues);
        }
    }
}
