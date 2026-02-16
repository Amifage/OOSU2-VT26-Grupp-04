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
        }

        private void RensaFormulär()
        {
            NamnTextBox.Text = "";
            EpostTextBox.Text = "";
            TelefonTextBox.Text = "";


            MedlemsIDTextBox.Focus(); //Denna raden flyttar fokus/pekaren tillbaka till medlem namn så man direkt kan registrera en ny medlem
        }

        private void SumbitMedlemIDButton_Click(object sender, RoutedEventArgs e)
        {

            if (!int.TryParse(MedlemsIDTextBox.Text?.Trim(), out int id))
            {
                MessageBox.Show("Skriv ett giltigt medlems-ID (heltal)."); //Vill vi ha hetal med här?
                return;
            }

            medlem = _medlemController.HamtaMedlemById(id);

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

            medlem.Namn = NamnTextBox.Text.Trim().ToLower();
            medlem.Epost = string.IsNullOrWhiteSpace(EpostTextBox.Text) ? null : EpostTextBox.Text.Trim().ToLower();
            medlem.Telefonnummer = string.IsNullOrWhiteSpace(TelefonTextBox.Text) ? null : TelefonTextBox.Text.Trim();
            medlem.SenastUppdaterad = DateTime.Now;

            int rows = _medlemController.UppdateraMedlem(medlem);

            MessageBox.Show(rows == 1 ? "Medlem uppdaterad!" : "Något gick fel");
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
                RensaFormulär();
            }
            else
            {
                MessageBox.Show("Radering misslyckades (medlem hittades inte eller fel uppstod).");
            }
        }
    }
}
