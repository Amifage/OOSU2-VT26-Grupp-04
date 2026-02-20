using Affärslagret;
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
using System.Windows.Shapes;
using Entitetslager;

namespace Presentationslager
{
    /// <summary>
    /// Interaction logic for RegistreraResurs.xaml
    /// </summary>
    public partial class RegistreraResurs : Window
    {
        private readonly ResursController _resursController = new ResursController();
        private Resurs? _skapadResurs;

        public RegistreraResurs()
        {
            InitializeComponent();
        }

        #region Kod för utrustning
        private void LaddaKoppladUtrustning() //hämtar alla utrustnigar som är kopplade till resursen
        {
            if (_skapadResurs == null)
            {
                KoppladListView.ItemsSource = null;
                return;
            }

            KoppladListView.ItemsSource = _resursController
                .HämtaUtrustningFörResurs(_skapadResurs.ResursID)
                .OrderBy(u => u.Namn)
                .ToList();
        }

        private void LaddaOkoppladUtrustning() //hämtar all utrustning som inte är kopplad till någon resurs
        {
            OkoppladListView.ItemsSource = _resursController
                .HämtaOkoppladUtrustning()
                .OrderBy(u => u.Namn)
                .ToList();
        }

        private void KopplaButton_Click(object sender, RoutedEventArgs e) // kopplar utrustning
        {
            if (_skapadResurs == null)
            {
                MessageBox.Show("Välj en resurs först.");
                return;
            }

            var valda = OkoppladListView.SelectedItems.Cast<Utrustning>().ToList();
            if (valda.Count == 0)
            {
                MessageBox.Show("Markera utrustning i listan 'Okopplad'.");
                return;
            }

            var ids = valda.Select(u => u.Inventarienummer).ToList();
            int antal = _resursController.KopplaUtrustningTillResurs(_skapadResurs.ResursID, ids);

            MessageBox.Show($"Kopplade {antal} st.");
            LaddaKoppladUtrustning();
            LaddaOkoppladUtrustning();
        }

        private void AvkopplaButton_Click(object sender, RoutedEventArgs e) //bortkopplar utrustning
        {
            var valda = KoppladListView.SelectedItems.Cast<Utrustning>().ToList();
            if (valda.Count == 0)
            {
                MessageBox.Show("Markera utrustning i listan 'Kopplad'.");
                return;
            }

            var ids = valda.Select(u => u.Inventarienummer).ToList();
            int antal = _resursController.AvkopplaUtrustningFrånResurs(ids);

            MessageBox.Show($"Tog bort koppling för {antal} st.");
            LaddaKoppladUtrustning();
            LaddaOkoppladUtrustning();
        }
        #endregion

        private void SparaNyResursButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string namn = ResursNamnTextBox.Text.Trim().ToLower();
                string typ = (ResurstypComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString().ToLower();
                bool okKapacitet = int.TryParse(ResursKapacitetTextBox.Text?.Trim(), out int kapacitet);
                string status = "tillgänglig";

          
                if (string.IsNullOrWhiteSpace(namn) || (string.IsNullOrWhiteSpace(typ)))
                {
                    MessageBox.Show("Du måste fylla i resursnamn.");
                    return;
                }

                if (!okKapacitet || kapacitet < 0) 
                {
                    MessageBox.Show("Kapacitet måste vara ett heltal (0 eller högre).");
                    return;
                }

                var nyResurs = new Resurs
                {
                    Namn = namn,
                    Typ = typ,
                    Kapacitet = kapacitet,
                    Status = status,
                    SenastUppdaterad = DateTime.Now
                };

                _resursController.SkapaResurs(nyResurs);
                _skapadResurs = nyResurs;

                MessageBox.Show($"Resursen har sparats! \n\nTilldelat resursID: {nyResurs.ResursID}");

                LaddaKoppladUtrustning();
                LaddaOkoppladUtrustning();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel vid sparning: " + ex.Message);
            }
        }
    }
    
}
