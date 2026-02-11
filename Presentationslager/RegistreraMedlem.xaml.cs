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

        private void SparaNyMedlemButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string namn = MedlemNamnTextBox.Text?.Trim();
                string epost = MedlemsEpostTextBox.Text?.Trim();
                string telefon = MedlemsTelefonnummerTextBox.Text?.Trim();

                string medlemsnivå = (MedlemsNivåComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();
                string betalstatus = (MedlemsBetalstatusComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

                // Validering
                if (string.IsNullOrWhiteSpace(namn)) //Här behöver epost och teelfon också valideras
                {
                    MessageBox.Show("Du måste fylla i namn/företagsnamn.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(medlemsnivå) || string.IsNullOrWhiteSpace(betalstatus))
                {
                    MessageBox.Show("Välj medlemsnivå och betalstatus.");
                    return;
                }

                var nyMedlem = new Medlem
                {
                    Namn = namn,
                    Epost = string.IsNullOrWhiteSpace(epost) ? null : epost,
                    Telefonnummer = string.IsNullOrWhiteSpace(telefon) ? null : telefon,
                    Medlemsnivå = medlemsnivå,
                    Betalstatus = betalstatus,
                    SenastUppdaterad = DateTime.Now
                };

                _medlemController.SkapaMedlem(nyMedlem);

                MessageBox.Show($"Medlemmen har sparats!");

              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel vid sparning: " + ex.Message);
            }

        }
                         
    }
    
    
}
