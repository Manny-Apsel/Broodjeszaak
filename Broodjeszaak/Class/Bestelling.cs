using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broodjeszaak.Class
{
    internal class Bestelling
    {
        public int Nummer { get; set; }
        public string Status { get; set; }
        public string BroodSoort { get; set; } = string.Empty;
        public string Beleg { get; set; } = string.Empty;
        public string Saus { get; set; } = string.Empty;
        public bool Smos { get; set; } = false;
        public double Prijs { get; set; } = 0;

        public void calculatePrice(Dictionary<string, double> priceList)
        {
            double price = priceList[this.BroodSoort];
            price += priceList[this.Beleg];
            price += priceList[this.Saus];
            if (this.Smos)
            {
                price += priceList["Smos"];
            }

            this.Prijs = Math.Round(price, 2);
        }

        
    }
}
