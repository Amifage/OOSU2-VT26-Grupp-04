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

        public RegistreraResurs()
        {
            InitializeComponent();
        }

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

                MessageBox.Show("Resursen har sparats!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel vid sparning: " + ex.Message);
            }
        }
    }
    
}
