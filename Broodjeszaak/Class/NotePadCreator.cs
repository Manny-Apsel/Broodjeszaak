using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace Broodjeszaak.Class
{
    static internal class NotePadCreator
    {
        static internal void CreateNotePad(Bestelling input)
        {
            var dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var date = DateTime.Now;
            var fileName = $"RekeningBroodjeszaak_{date.Ticks}_{input.Nummer}.txt";
            var filePath = $"{dir}\\{fileName}";


            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (FileStream fs = File.Create(filePath))
                {
                    var text = $"Rekening broodjeszaak\r\n";
                    text += "======================\r\n";
                    text += $"{date.ToShortDateString()}\r\n";
                    text += $"Bestelling:\t{input.Nummer}\r\n";
                    text += "\r\n";
                    text += $"{input.BroodSoort}\r\n" ;
                    if (input.Beleg != null)
                    {
                        text += $"{input.Beleg}\r\n";
                    }
                    if (input.Saus != "geen") 
                    {
                        text += $"{input.Saus}\r\n";
                    }
                    if (input.Smos == true)
                    {
                        text += $"Smos\r\n";
                    }
                    text += "\r\n";
                    text += $"Totale prijs: \t {String.Format("{0:0.00}", input.Prijs)}";
                    Byte[] txt = new UTF8Encoding(true).GetBytes(text);
                    fs.Write(txt, 0, txt.Length);
                }

                var p = new Process();
                p.StartInfo = new ProcessStartInfo(fileName)
                {
                    UseShellExecute = true
                };
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
