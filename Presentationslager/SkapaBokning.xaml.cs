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
        private readonly MedlemController _medlemController = new MedlemController();
       
        public SkapaBokning()
        {
            InitializeComponent();
            this.LoadTimePicker();
            Loaded += UppdateraMedlem_Loaded;
        }
        #region Kod för medlem combobox
        private void UppdateraMedlem_Loaded(object sender, RoutedEventArgs e)
        {
            LaddaMedlemmar();
        }

        private void LaddaMedlemmar()
        {
            var medlemmar = _medlemController.HämtaAllaMedlemmar() //laddar in medlemmar i comboboxen
                                             .OrderBy(m => m.Namn)
                                             .ToList();

            MedlemComboBox.ItemsSource = medlemmar;
            MedlemComboBox.SelectedIndex = -1;

        }

        private void MedlemComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) //metod som registrerar valet från comboboxen
        {
            if (MedlemComboBox.SelectedItem is not Medlem vald)
                return;

            MedlemsIDTextBox.Text = vald.MedlemID.ToString();
        }

        private void MedlemsIDTextBox_TextChanged(object sender, TextChangedEventArgs e) // skapar en automatisk koppling mellan textrutan för medlems-ID och comboboxen
        {
            if (!int.TryParse(MedlemsIDTextBox.Text, out int id))
                return;

            var match = (MedlemComboBox.ItemsSource as IEnumerable<Medlem>)
                ?.FirstOrDefault(m => m.MedlemID == id);

            MedlemComboBox.SelectedItem = match;
        }
        #endregion

   
        private void LoadTimePicker() //sätter värden för tid coboboxerna
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

       
        private void SökLedigaButton_Click(object sender, RoutedEventArgs e)  // Metod för att söka efter lediga resurser
        {
            DateTime? start = GetBokningsDateTime();
            if (!start.HasValue)
            {
                MessageBox.Show("Välj datum och tid först.");
                return;
            }

            
            if (!int.TryParse(LängdTextBox.Text, out int timmar)) timmar = 1; // Hämta längd från textbox, standardvärde 1 timme om inmatning saknas
            DateTime slut = start.Value.AddHours(timmar);

        
            var lediga = _resursController.HämtaLedigaResurser(start.Value, slut); // Anropar controllern för att filtrera fram lediga resurser

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
        private DateTime? GetBokningsDateTime() // Metod för att skapa DateTime från valen i kalendern och tid-comboboxarna
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

        private void SparaBokningButton_Click(object sender, RoutedEventArgs e) // Metod för att spara den nya bokningen
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

                var nyBokning = new Bokning // sätetr värden för den nya bokningen
                {
                    MedlemID = medlemId,
                    ResursID = valdResurs.ResursID,
                    Starttid = start,
                    Sluttid = start.AddHours(timmar),
                    SenastUppdaterad = DateTime.Now,
                    Anteckning = AnteckningTextBox.Text 
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
              
    }
}
