using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CsvHelper;
using CsvHelper.Configuration;

namespace Broodjeszaak.Class
{
    static internal class ExcelConnection
    {
       static internal Dictionary<string, double> GetPrices()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                NewLine = Environment.NewLine,
            };

            List<Price> records = new List<Price>();
            

            var dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            var filePath = dir + @"\Broodjesprijzen[14915].csv";

            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = csv.GetRecords<Price>().ToList();
                    Dictionary<string, double> priceList = new Dictionary<string, double>();
                    priceList.Add("", 0);
                    foreach (var record in records)
                    {
                        priceList.Add(record.Item, record.prijs);
                    }

                    return priceList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new Dictionary<string, double>();
        }
    }
}
