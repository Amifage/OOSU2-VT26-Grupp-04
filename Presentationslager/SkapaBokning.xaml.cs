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
        public SkapaBokning()
        {
            InitializeComponent();
            this.LoadTimePicker();
        }
        private void LoadTimePicker()
        {
            // Fyll timmar 00-23
            for (int hour = 0; hour <= 23; hour++)
            {
                this.TimmarComboBox.Items.Add(hour.ToString("D2"));
            }

            // Fyll minuter 00-59 (i 5-min steg)
            for (int minute = 0; minute <= 59; minute += 5)
            {
                this.MinuterComboBox.Items.Add(minute.ToString("D2"));
            }

            // Standardval
            this.TimmarComboBox.SelectedIndex = 12;  // 12
            this.MinuterComboBox.SelectedIndex = 0;  // 00
        }
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
