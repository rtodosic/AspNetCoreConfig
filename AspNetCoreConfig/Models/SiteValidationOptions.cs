using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreConfig.Models
{
    public class SiteValidationOptions
    {
        [Required(ErrorMessage = "The Title required")]
        public string Title { get; set; }
        public string Url { get; set; }
        public string Key { get; set; }
    }
}
