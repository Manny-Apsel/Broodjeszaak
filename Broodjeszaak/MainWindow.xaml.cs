using Broodjeszaak.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Broodjeszaak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // can't use var in here???
        Bestelling bestelling = new Bestelling();
        Dictionary<string, double> prices = ExcelConnection.GetPrices();
        List<Bestelling> bestellingen = new List<Bestelling>();

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void LBBeleg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LBBeleg.SelectedIndex > -1)
            {
                var lbItem = (ListBoxItem)LBBeleg.SelectedItem;
                var text = lbItem.Content.ToString();
                bestelling.Beleg = text;
                bestelling.calculatePrice(prices);
                TBPrijs.Text = String.Format("{0:0.00}", bestelling.Prijs);
            }
        }

        private void RBBroodWit_Checked(object sender, RoutedEventArgs e)
        {
            var text = RBBroodWit.Content.ToString();
            bestelling.BroodSoort = text;
            bestelling.calculatePrice(prices);
            TBPrijs.Text = String.Format("{0:0.00}", bestelling.Prijs);
        }

        private void RBBroodBruin_Checked(object sender, RoutedEventArgs e)
        {
            var text = RBBroodBruin.Content.ToString();
            bestelling.BroodSoort = text;
            bestelling.calculatePrice(prices);
            TBPrijs.Text = String.Format("{0:0.00}", bestelling.Prijs);
        }

        private void RBBroodWalkorn_Checked(object sender, RoutedEventArgs e)
        {
            var text = RBBroodWalkorn.Content.ToString();
            bestelling.BroodSoort = text;
            bestelling.calculatePrice(prices);
            TBPrijs.Text = String.Format("{0:0.00}", bestelling.Prijs);
        }

        private void RBBroodMultiGranen_Checked(object sender, RoutedEventArgs e)
        {
            var text = RBBroodMultiGranen.Content.ToString();
            bestelling.BroodSoort = text;
            bestelling.calculatePrice(prices);
            TBPrijs.Text = String.Format("{0:0.00}", bestelling.Prijs);
        }
        private void RBBroodGeen_Checked(object sender, RoutedEventArgs e)
        {
            // when this radiobutton is checked it says during installation that object isn't initialized?
            var text = RBBroodGeen.Content.ToString();
            bestelling.BroodSoort = text;
            bestelling.calculatePrice(prices);
            TBPrijs.Text = String.Format("{0:0.00}", bestelling.Prijs);
        }

        private void CBSaus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var text = (CBSaus.SelectedItem as ComboBoxItem).Content.ToString();
            bestelling.Saus = text;
            bestelling.calculatePrice(prices);
            TBPrijs.Text = String.Format("{0:0.00}", bestelling.Prijs);
        }

        private void CBSmos_Click(object sender, RoutedEventArgs e)
        {
            var smosIsChecked = (bool)CBSmos.IsChecked;
            bestelling.Smos = smosIsChecked;
            bestelling.calculatePrice(prices);
            TBPrijs.Text = String.Format("{0:0.00}", bestelling.Prijs);
        }

        private void BTNBestellingCreatie_Click(object sender, RoutedEventArgs e)
        {
            if (bestelling.BroodSoort == "Geen" || bestelling.BroodSoort == string.Empty) 
            {
                MessageBox.Show("Gelieve een broodje te selecteren.");
                return;
            }

            var beschrijving = $"Een {bestelling.BroodSoort.ToLower()} broodje";
            if (bestelling.Smos == true)
            {
                beschrijving += " met smos";
            }
            if(bestelling.Beleg != string.Empty)
            {
                beschrijving += $" met als beleg {bestelling.Beleg.ToLower()}";
            }
            if (bestelling.Saus != "Geen")
            {
                beschrijving += $" met als saus {bestelling.Saus.ToLower()}";
            }

            beschrijving += ".\r\n";

            beschrijving += $"Kostprijs: \t{String.Format("{0:0.00}", bestelling.Prijs)} EUR";

            TBBestellingBeschrijving.Text = beschrijving;
            if (bestellingen.Count > 0) 
            {
                bestelling.Nummer = bestellingen.Last().Nummer + 1;
            }
            else
            {
                bestelling.Nummer = 1;
            }
            bestelling.Status = "To Do";
            bestellingen.Add(bestelling);

            // reset keuzes
            bestelling = new Bestelling();
            RBBroodGeen.IsChecked = false;
            RBBroodWit.IsChecked = false;
            RBBroodBruin.IsChecked = false;
            RBBroodWalkorn.IsChecked = false;
            RBBroodMultiGranen.IsChecked = false;
            CBSaus.SelectedIndex = 0;
            CBSmos.IsChecked = false;
            LBBeleg.UnselectAll();
            populateGrids();
        }

        private void populateGrids()
        {
            DGUitTeVoerenBestellingen.ItemsSource = bestellingen.Where(x => x.Status == "To Do");
            DGBestellingenUitGevoerd.ItemsSource = bestellingen.Where(x => x.Status == "Done");
        }

        private void BTNVoerBestellingUit_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Bestelling)DGUitTeVoerenBestellingen.SelectedItem;
            bestellingen.Where(x => x.Nummer == selectedItem.Nummer).ToList().ForEach(y => y.Status = "Done");
            populateGrids();
            // notepad ticket aanmaken
            NotePadCreator.CreateNotePad(selectedItem);

        }

        private void BTNAnnulleerBestelling_Click(object sender, RoutedEventArgs e)
        {
            var selectedBestelling = (Bestelling)DGUitTeVoerenBestellingen.SelectedItem;
            bestellingen.Remove(selectedBestelling);
            populateGrids();
        }
    }
}
