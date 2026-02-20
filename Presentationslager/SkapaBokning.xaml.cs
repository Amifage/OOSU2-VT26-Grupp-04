using Affärslagret;
using Entitetslager;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for SkapaBokning.xaml
    /// </summary>
    public partial class SkapaBokning : Window
    {
        private readonly ResursController _resursController = new ResursController();
        private readonly BokningController _bokningController = new BokningController();
        public SkapaBokning()
        {
            InitializeComponent();
            this.LoadTimePicker();
        } 

        // Din befintliga metod för att ladda tider
        private void LoadTimePicker()
        {
            for (int hour = 0; hour <= 23; hour++)
            {
                this.TimmarComboBox.Items.Add(hour.ToString("D2"));
            }

            for (int minute = 0; minute <= 59; minute += 5)
            {
                this.MinuterComboBox.Items.Add(minute.ToString("D2"));
            }

            this.TimmarComboBox.SelectedIndex = 12;
            this.MinuterComboBox.SelectedIndex = 0;
        }

        // Metod för att söka efter lediga resurser
        private void SökLedigaButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime? start = GetBokningsDateTime();
            if (!start.HasValue)
            {
                MessageBox.Show("Välj datum och tid först.");
                return;
            }

            // Hämta längd från TextBox, standardvärde 1 timme om inmatning saknas
            if (!int.TryParse(LängdTextBox.Text, out int timmar)) timmar = 1;
            DateTime slut = start.Value.AddHours(timmar);

            // Anropar kontrollern för att filtrera fram lediga resurser
            var lediga = _resursController.HämtaLedigaResurser(start.Value, slut);

            VäljresursComboBox.ItemsSource = lediga;
           

            if (lediga.Any())
            {
                SparaBokningButton.IsEnabled = true;             
            }
            else
            {
                SparaBokningButton.IsEnabled = false;
                MessageBox.Show("Inga lediga resurser hittades för denna tid.");
            }
        }

        // Metod för att spara den nya bokningen
        private void SparaBokningButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(MedlemsIDTextBox.Text, out int medlemId))
                {
                    MessageBox.Show("Ange ett giltigt Medlems-ID.");
                    return;
                }

                var valdResurs = VäljresursComboBox.SelectedItem as Resurs;
                if (valdResurs == null)
                {
                    MessageBox.Show("Välj en resurs i listan.");
                    return;
                }


                DateTime start = GetBokningsDateTime().Value;
                int timmar = int.Parse(LängdTextBox.Text);

                var nyBokning = new Bokning
                {
                    MedlemID = medlemId,
                    ResursID = valdResurs.ResursID,
                    Starttid = start,
                    Sluttid = start.AddHours(timmar),
                    SenastUppdaterad = DateTime.Now,
                    Anteckning = AnteckningTextBox.Text //Anteckning för att lägga in medlemar
                };

                _bokningController.SkapaBokning(nyBokning);
                MessageBox.Show("Bokningen har skapats!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ett fel uppstod: " + ex.Message);
            }
        }
        // Din befintliga metod för att skapa DateTime från valen i fönstret
        private DateTime? GetBokningsDateTime()
        {
            DateTime? selectedDate = this.BokningsCalander.SelectedDate;

            if (selectedDate == null)
            {
                return null;
            }

            if (this.TimmarComboBox.SelectedItem == null || this.MinuterComboBox.SelectedItem == null)
            {
                return null;
            }

            int hour;
            int minute;

            if (!int.TryParse(this.TimmarComboBox.SelectedItem.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out hour))
            {
                return null;
            }

            if (!int.TryParse(this.MinuterComboBox.SelectedItem.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out minute))
            {
                return null;
            }

            DateTime result = new DateTime(
                selectedDate.Value.Year,
                selectedDate.Value.Month,
                selectedDate.Value.Day,
                hour,
                minute,
                0);

            return result;
        }
    }
}
