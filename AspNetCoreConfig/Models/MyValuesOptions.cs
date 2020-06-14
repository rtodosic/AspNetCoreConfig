using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreConfig.Models
{
    public class AboutInformation
    {
        public string Author { get; set; }
        public string Version { get; set; }
    }

    public class MyValuesOptions
    {
        public string Value1 { get; set; }
        public int Value2 { get; set; }
        public bool Value3 { get; set; }

        public AboutInformation About { get; set; }
    }
}
