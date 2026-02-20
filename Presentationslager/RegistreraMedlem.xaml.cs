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
using Affärslagret;

namespace Presentationslager
{
    /// <summary>
    /// Interaction logic for RegistreraMedlem.xaml
    /// </summary>
    public partial class RegistreraMedlem : Window
    {

        private readonly MedlemController _medlemController = new MedlemController();

        public RegistreraMedlem()
        {
            InitializeComponent();
        }
        private void RensaFormulär()
        {
            MedlemNamnTextBox.Text = "";
            MedlemsEpostTextBox.Text = "";
            MedlemsTelefonnummerTextBox.Text = "";

            MedlemsNivåComboBox.SelectedIndex = -1;
            MedlemsBetalstatusComboBox.SelectedIndex = -1;

            MedlemNamnTextBox.Focus(); //Denna raden flyttar fokus/pekaren tillbaka till medlem namn så man direkt kan registrera en ny medlem
        }
        static bool IsDigitsOnly(string str) //Denna funktion tar en sträng och returnerar falskt om villkoret inte stämmer och sant om villkoret stämmer. Funktionen kontrollerar så strängen innehåller siffror mellan 0-9.
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
        private void SparaNyMedlemButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string namn = MedlemNamnTextBox.Text.Trim().ToLower();
                string epost = MedlemsEpostTextBox.Text.Trim().ToLower();
                string telefon = MedlemsTelefonnummerTextBox.Text.Replace(" ", "").Trim(); //tar bort alla mellanrum i telefonnummer

                string medlemsnivå = (MedlemsNivåComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString().ToLower();
                string betalstatus = (MedlemsBetalstatusComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString().ToLower() ;

                // Validering
                if (string.IsNullOrWhiteSpace(namn) || string.IsNullOrWhiteSpace(epost) || string.IsNullOrWhiteSpace(telefon))
                {
                    MessageBox.Show("Du måste fylla i samtliga uppgifter.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(medlemsnivå) || string.IsNullOrWhiteSpace(betalstatus))
                {
                    MessageBox.Show("Välj medlemsnivå och betalstatus.");
                    return;
                }
                if (!IsDigitsOnly(telefon)) 
                {
                    MessageBox.Show("Telefonnummer får bara innehålla siffror.");
                    return;
                }

                var nyMedlem = new Medlem
                {
                    Namn = namn,
                    Epost = epost,
                    Telefonnummer = telefon,
                    Medlemsnivå = medlemsnivå,
                    Betalstatus = betalstatus,
                    SenastUppdaterad = DateTime.Now
                };

                _medlemController.SkapaMedlem(nyMedlem);

                MessageBox.Show($"Medlemmen har sparats!\n\nTilldelat medlemsnummer: {nyMedlem.MedlemID}");

                RensaFormulär();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel vid sparning: " + ex.Message);
            }

        }
                         
    }

}
