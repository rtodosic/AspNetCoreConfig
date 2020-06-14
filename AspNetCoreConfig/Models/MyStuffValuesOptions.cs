using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreConfig.Models
{
    public interface IMyValuesConfiguration
    {
        string Value1 { get; }
        int Value2 { get; }
        bool Value3 { get; }
    }

    public class MyValuesConfiguration : IMyValuesConfiguration
    {
        public string Value1 { get; set; }
        public int Value2 { get; set; }
        public bool Value3 { get; set; }
    }
}
