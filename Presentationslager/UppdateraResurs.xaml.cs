using Affärslagret;
using Datalager;
using Entitetslager;
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

namespace Presentationslager
{
    /// <summary>
    /// Interaction logic for UppdateraResurs.xaml
    /// </summary>
    public partial class UppdateraResurs : Window
    {

        private readonly ResursController _resursController = new ResursController();
        private Resurs? resurs;
        public UppdateraResurs()
        {
            InitializeComponent();
            Loaded += UppdateraResurs_Loaded;         
        }

        private void RensaFormulär()
        {
            ResursIDTextBox.Text = "";
            NamnTextBox.Text = "";
            TypTextBox.Text = "";
            KapacitetTextBox.Text = "";
            ResursComboBox.SelectedIndex = -1;
            UtrustningComboBox.ItemsSource = null;
            UtrustningComboBox.SelectedIndex = -1;
            resurs = null;
            NamnTextBox.Focus();
        }

        #region Kod för utrsutning
        private void LaddaKoppladUtrustning()
        {
            if (resurs == null)
            {
                KoppladListView.ItemsSource = null;
                return;
            }

            KoppladListView.ItemsSource = _resursController
                .HämtaUtrustningFörResurs(resurs.ResursID)
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
            if (resurs == null)
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
            int antal = _resursController.KopplaUtrustningTillResurs(resurs.ResursID, ids);

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

        private void LaddaUtrustningFörValdResurs()
        {
            if (resurs == null)
            {
                UtrustningComboBox.ItemsSource = null;
                return;
            }

            var utrustning = _resursController.HämtaUtrustningFörResurs(resurs.ResursID)
                                              .OrderBy(u => u.Namn)
                                              .ToList();

            UtrustningComboBox.ItemsSource = utrustning;
            UtrustningComboBox.SelectedIndex = -1;
        }
        #endregion

        #region Kod för combobox
        private void UppdateraResurs_Loaded(object sender, RoutedEventArgs e)
        {
            LaddaResurser();
        }
     
        private void LaddaResurser()
        {
            var resurser = _resursController.HämtaAllaResurser()
                                            .OrderBy(r => r.Namn)
                                            .ToList();

            ResursComboBox.ItemsSource = resurser;
            ResursComboBox.SelectedIndex = -1;
        }

        private void ResursComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ResursComboBox.SelectedItem is not Resurs vald)
                return;

            resurs = vald;

            ResursIDTextBox.Text = resurs.ResursID.ToString();
            NamnTextBox.Text = resurs.Namn ?? "";
            TypTextBox.Text = resurs.Typ ?? "";
            KapacitetTextBox.Text = resurs.Kapacitet.ToString();

            LaddaUtrustningFörValdResurs();

            LaddaKoppladUtrustning();
            LaddaOkoppladUtrustning();

        }
        #endregion

        private void SumbitResursIDButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ResursIDTextBox.Text?.Trim(), out int id))
            {
                MessageBox.Show("Skriv ett giltigt resurs-ID (heltal)."); 
                return;
            }
          
           resurs = _resursController.HämtaResursById(id);

            if (resurs == null)
            {
                MessageBox.Show("Ingen resurs hittades med det ID:t.");
                return;
            }

            NamnTextBox.Text = resurs.Namn;
            TypTextBox.Text = resurs.Typ ?? "";
            KapacitetTextBox.Text = resurs.Kapacitet.ToString();

            LaddaUtrustningFörValdResurs();

            LaddaKoppladUtrustning();
            LaddaOkoppladUtrustning();

        }

        private void SparaÄndradResursButton_Click(object sender, RoutedEventArgs e)
        {
            if (resurs == null)
            {
                MessageBox.Show("Hämta resursen först");
                return;
            }

            resurs.Namn =NamnTextBox.Text.Trim().ToLower();
            resurs.Typ = TypTextBox.Text.Trim().ToLower();
            if (int.TryParse(KapacitetTextBox.Text, out int antal))
            {
                resurs.Kapacitet = antal;
            }
            else
            {
                MessageBox.Show("Vänligen ange kapacitet i siffror");
                return;
            }
            resurs.SenastUppdaterad = DateTime.Now;

            int rows = _resursController.UppdateraResurs(resurs);


            MessageBox.Show(rows == 1 ? "Resurs uppdaterad!" : "Något gick fel");
            
            LaddaResurser();
            RensaFormulär();

        }

        private void RaderaResursButton_Click(object sender, RoutedEventArgs e)
        {
            if (resurs == null)
            {
                MessageBox.Show("Hämta resurs först.");
                return;
            }

            var result = MessageBox.Show(
                $"Är du säker på att du vill radera resursen?\n\n" +
                $"ID: {resurs.ResursID}\n" +
                $"Namn: {resurs.Namn}\n" +
                $"Typ: {resurs.Typ}", 
                "Bekräfta radering",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            int rows = _resursController.TaBortResurs(resurs.ResursID);

            if (rows == 1)
            {
                MessageBox.Show("Resurs raderad!");
                resurs = null;
                LaddaResurser();
                RensaFormulär();
            }
            else
            {
                MessageBox.Show("Radering misslyckades (resurs hittades inte eller fel uppstod).");
            }
        }
    }
}
