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
    /// Interaction logic for MedlemsAktivitet.xaml
    /// </summary>
    public partial class MedlemsAktivitet : Window
    {
        private readonly MedlemController _medlemController = new MedlemController();
        private readonly BokningController _bokningController = new BokningController();
        private Medlem? medlem;
        public MedlemsAktivitet()
        {
            InitializeComponent();
            Loaded += UppdateraMedlem_Loaded; 
        }

        #region Kod för medlem combobox
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

            MedlemsIDTextBox.Text = vald.MedlemID.ToString();
        }

        private void MedlemsIDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(MedlemsIDTextBox.Text, out int id))
                return;

            var match = (MedlemComboBox.ItemsSource as IEnumerable<Medlem>)
                ?.FirstOrDefault(m => m.MedlemID == id);

            MedlemComboBox.SelectedItem = match;
        }
        #endregion

        private void SumbitMedlemIDButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(MedlemsIDTextBox.Text?.Trim(), out int id))
            {
                MessageBox.Show("Skriv ett giltigt medlems-ID (heltal)."); 
                return;
            }
            medlem = _medlemController.HamtaMedlemById(id);

            if (medlem == null)
            {
                MessageBox.Show("Ingen medlem hittades med det ID:t.");
                return;
            }

            var bokningar = _bokningController.HämtaBokningarFörMedlem(id);
            MedlemsaktivitetDataGrid.ItemsSource = bokningar;
            if (bokningar.Count == 0 )
            {
                MessageBox.Show("Medlemmen hittades, men har inga registrerade bokningar");
            }
           
        }
    }
}
