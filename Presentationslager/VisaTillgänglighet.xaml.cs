using Affärslagret;
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
    /// Interaction logic for VisaTillgänglighet.xaml
    /// </summary>
    public partial class VisaTillgänglighet : Window
    {
        private readonly ResursController _resursController = new ResursController();
        public VisaTillgänglighet()
        {
            InitializeComponent();
            LaddaTidsVäljare();
        }
        private void LaddaTidsVäljare()
        {
         
            for (int i = 0; i <= 23; i++) TimmarComboBox.Items.Add(i.ToString("D2"));
            for (int i = 0; i <= 55; i += 5) MinuterComboBox.Items.Add(i.ToString("D2"));

            TimmarComboBox.SelectedIndex = 12;
            MinuterComboBox.SelectedIndex = 0;
            Kalender.SelectedDate = DateTime.Today;
        }

        private void SökTillgänglighetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Kalender.SelectedDate == null)
                {
                    MessageBox.Show("Vänligen välj ett datum.");
                    return;
                }

          
                DateTime datum = Kalender.SelectedDate.Value;
                int timme = int.Parse(TimmarComboBox.SelectedItem.ToString());
                int minut = int.Parse(MinuterComboBox.SelectedItem.ToString());
                DateTime startTid = new DateTime(datum.Year, datum.Month, datum.Day, timme, minut, 0);

              
                if (!int.TryParse(LängdTextBox.Text, out int timmar)) timmar = 1;
                DateTime slutTid = startTid.AddHours(timmar);

              
                List<Resurs> lediga = _resursController.HämtaLedigaResurser(startTid, slutTid);

               
                TillgängligaResurserGrid.ItemsSource = lediga;

                if (lediga.Count == 0)
                {
                    MessageBox.Show("Inga lediga resurser hittades för den valda tidpunkten.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ett fel uppstod: " + ex.Message);
            }
        }
    }

}
