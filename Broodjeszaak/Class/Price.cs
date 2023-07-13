using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broodjeszaak.Class
{
    internal class Price
    {
        [Index(0)]
        public string Item { get; set; }
        [Index(1)]
        public double prijs { get; set; }
    }
}
