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
    /// Interaction logic for HanteraBokning.xaml
    /// </summary>
    public partial class HanteraBokning : Window
    {
        private BokningController _bokningController = new BokningController();
        private MedlemController _medlemController = new MedlemController(); // För att hämta medlemmar
        private ResursController _resursController = new ResursController(); // För att hämta resurser
        private Bokning valdBokning;
        public HanteraBokning()
        {
            InitializeComponent();
            LaddaData();
        }

        private void LaddaData()
        {
            BokningsDataGrid.ItemsSource = _bokningController.HämtaKommandeBokningar();
            // Du behöver lägga till metoder i dina controllers för att hämta ALLA medlemmar/resurser
            // MedlemComboBox.ItemsSource = _medlemController.HämtaAllaMedlemmar(); 
            // ResursComboBox.ItemsSource = _resursController.HämtaAllaResurser();
        }

        private void BokningsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            valdBokning = BokningsDataGrid.SelectedItem as Bokning;
            if (valdBokning != null)
            {
                StarttidTextBox.Text = valdBokning.Starttid.ToString("yyyy-MM-dd HH:mm");
                SluttidTextBox.Text = valdBokning.Sluttid.ToString("yyyy-MM-dd HH:mm");
                MedlemComboBox.SelectedItem = valdBokning.medlem;
                ResursComboBox.SelectedItem = valdBokning.resurs;
            }
        }

        private void SparaButton_Click(object sender, RoutedEventArgs e)
        {
            if (valdBokning == null) return;

            var valdMedlem = MedlemComboBox.SelectedItem as Medlem;
            var valdResurs = ResursComboBox.SelectedItem as Resurs;

            if (valdMedlem != null) valdBokning.MedlemID = valdMedlem.MedlemID;
            if (valdResurs != null) valdBokning.ResursID = valdResurs.ResursID;

            valdBokning.Starttid = DateTime.Parse(StarttidTextBox.Text);
            valdBokning.Sluttid = DateTime.Parse(SluttidTextBox.Text);
            valdBokning.SenastUppdaterad = DateTime.Now;

            _bokningController.UppdateraBokning(valdBokning);
            MessageBox.Show("Bokningen uppdaterad!");
            LaddaData();
        }

        private void TaBortButton_Click(object sender, RoutedEventArgs e)
        {
            if (valdBokning == null) return;

            var resultat = MessageBox.Show("Vill du verkligen ta bort denna bokning?", "Bekräfta", MessageBoxButton.YesNo);
            if (resultat == MessageBoxResult.Yes)
            {
                _bokningController.TaBortBokning(valdBokning.BokningsID);
                MessageBox.Show("Bokningen raderad.");
                LaddaData();
            }

        }
    }
}
