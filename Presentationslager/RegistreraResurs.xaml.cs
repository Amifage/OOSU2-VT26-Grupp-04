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
        private Resurs? _skapadResurs;//NY
        //private Resurs? resurs;

        public RegistreraResurs()
        {
            InitializeComponent();
        }

        #region Kod för utrustning
        private void LaddaKoppladUtrustning()
        {
            if (_skapadResurs == null) //ändrat här
            {
                KoppladListView.ItemsSource = null;
                return;
            }

            KoppladListView.ItemsSource = _resursController
                .HämtaUtrustningFörResurs(_skapadResurs.ResursID) //Ändrat här från resurs
                .OrderBy(u => u.Namn)
                .ToList();
        }

        private void LaddaOkoppladUtrustning()
        {
            OkoppladListView.ItemsSource = _resursController
                .HämtaOkoppladUtrustning()
                .OrderBy(u => u.Namn)
                .ToList();
        }

        private void KopplaButton_Click(object sender, RoutedEventArgs e)
        {
            if (_skapadResurs == null) //ändrat här emd
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
            int antal = _resursController.KopplaUtrustningTillResurs(_skapadResurs.ResursID, ids); // Här med

            MessageBox.Show($"Kopplade {antal} st.");
            LaddaKoppladUtrustning();
            LaddaOkoppladUtrustning();
        }

        private void AvkopplaButton_Click(object sender, RoutedEventArgs e)
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

                // Validering
                if (string.IsNullOrWhiteSpace(namn) || (string.IsNullOrWhiteSpace(typ)))
                {
                    MessageBox.Show("Du måste fylla i resursnamn.");
                    return;
                }

                if (!okKapacitet || kapacitet < 0)  //Har redan validering så att ingen bokstav följer med av misstag.
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
