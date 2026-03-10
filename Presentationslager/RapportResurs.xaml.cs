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

namespace Presentationslager
{
    /// <summary>
    /// Interaction logic for RapportResurs.xaml
    /// </summary>
    public partial class RapportResurs : Window
    {
        private BokningController bokningController = new BokningController();
        public RapportResurs()
        {
            InitializeComponent();
            dpStartDatum.SelectedDate = DateTime.Now.AddMonths(-1); // Standard: Senaste månaden
        }

        private void btnUppdatera_Click(object sender, RoutedEventArgs e)
        {
            DateTime start = dpStartDatum.SelectedDate ?? DateTime.Now.AddMonths(-1);
            var statistik = bokningController.HämtaResursStatistik(start);

            dgResursStatistik.ItemsSource = statistik;
            lblTotaltAntal.Text = statistik.Sum(s => s.AntalBokningar).ToString();
            lblPopulärastResurs.Text = statistik.FirstOrDefault()?.Namn ?? "-";
        }
    }
}
