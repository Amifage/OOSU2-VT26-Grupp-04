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
    /// Interaction logic for UppdateraMedlem.xaml
    /// </summary>
    public partial class UppdateraMedlem : Window
    {
        private readonly MedlemController _medlemController = new MedlemController();
        private Medlem? medlem;
        public UppdateraMedlem()
        {
            InitializeComponent();
            Loaded += UppdateraMedlem_Loaded;
        }

        private void RensaFormulär()
        {
            MedlemsIDTextBox.Text = "";
            NamnTextBox.Text = "";
            EpostTextBox.Text = "";
            TelefonTextBox.Text = "";
            MedlemComboBox.SelectedIndex = -1;
            medlem = null;

            MedlemsIDTextBox.Focus(); 
        }

        static bool IsDigitsOnly(string str) 
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        #region Kod för combobox
        private void UppdateraMedlem_Loaded(object sender, RoutedEventArgs e) 
        {
            LaddaMedlemmar();
        }

        private void LaddaMedlemmar()
        {
            var medlemmar = _medlemController.HämtaAllaMedlemmar()
                                             .OrderBy(m => m.Namn)
                                             .ToList();

            MedlemComboBox.ItemsSource = medlemmar;
            MedlemComboBox.SelectedIndex = -1;

        }

        private void MedlemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MedlemComboBox.SelectedItem is not Medlem vald)
                return;

            medlem = vald;

            MedlemsIDTextBox.Text = medlem.MedlemID.ToString();
            NamnTextBox.Text = medlem.Namn ?? "";
            EpostTextBox.Text = medlem.Epost ?? "";
            TelefonTextBox.Text = medlem.Telefonnummer ?? "";
        }
        #endregion

        private void SumbitMedlemIDButton_Click(object sender, RoutedEventArgs e)
        {

            if (!int.TryParse(MedlemsIDTextBox.Text?.Trim(), out int id))
            {
                MessageBox.Show("Skriv ett giltigt medlems-ID (heltal)."); //Vill vi ha hetal med här?
                return;
            }

            medlem = _medlemController.HämtaMedlemById(id);

            if (medlem == null)
            {
                MessageBox.Show("Ingen medlem hittades med det ID:t.");
                return;
            }

            NamnTextBox.Text = medlem.Namn;
            EpostTextBox.Text = medlem.Epost ?? "";
            TelefonTextBox.Text = medlem.Telefonnummer ?? "";
           
        }
        private void SparaÄndradMedlemButton_Click(object sender, RoutedEventArgs e)
        {
            if (medlem == null)
            {
                MessageBox.Show("Hämta medlem först");
                return;
            }

            string telefon = TelefonTextBox.Text.Replace(" ", "").Trim();

            if (string.IsNullOrWhiteSpace(telefon))
            {
                MessageBox.Show("Telefonnummer måste fyllas i.");
                return;
            }

            if (!IsDigitsOnly(telefon))
            {
                MessageBox.Show("Telefonnummer får bara innehålla siffror.");
                return;
            }

            medlem.Namn = NamnTextBox.Text.Trim().ToLower();
            medlem.Epost = string.IsNullOrWhiteSpace(EpostTextBox.Text) ? null : EpostTextBox.Text.Trim().ToLower();
            medlem.Telefonnummer = string.IsNullOrWhiteSpace(TelefonTextBox.Text) ? null : TelefonTextBox.Text.Trim();
            medlem.SenastUppdaterad = DateTime.Now;

            int rows = _medlemController.UppdateraMedlem(medlem);

            MessageBox.Show(rows == 1 ? "Medlem uppdaterad!" : "Något gick fel");

            RensaFormulär();
        }
        private void RaderaMedlemButton_Click(object sender, RoutedEventArgs e)
        {
            if (medlem == null)
            {
                MessageBox.Show("Hämta medlem först.");
                return;
            }

            var result = MessageBox.Show(
                $"Är du säker på att du vill radera medlemmen?\n\n" +
                $"ID: {medlem.MedlemID}\n" +
                $"Namn: {medlem.Namn}\n" +
                $"E-post: {medlem.Epost}",
                "Bekräfta radering",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            int rows = _medlemController.TaBortMedlem(medlem.MedlemID);

            if (rows == 1)
            {
                MessageBox.Show("Medlem raderad!");
                medlem = null;
                LaddaMedlemmar();
                RensaFormulär();
            }
            else
            {
                MessageBox.Show("Radering misslyckades (medlem hittades inte eller fel uppstod).");
            }
        }
    }
}
