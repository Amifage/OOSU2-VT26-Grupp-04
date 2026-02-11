using Affärslagret;
using Datalager;
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
    /// Interaction logic for UppdateraMedlem.xaml
    /// </summary>
    public partial class UppdateraMedlem : Window
    {
        private readonly MedlemController _medlemController = new MedlemController();
        public UppdateraMedlem()
        {
            InitializeComponent();
        }

        private void SumbitMedlemIDButton_Click(object sender, RoutedEventArgs e)
        {


            if (!int.TryParse(MedlemsIDTextBox.Text?.Trim(), out int id))
            {
                MessageBox.Show("Skriv ett giltigt medlems-ID (heltal).");
                return;
            }

            Medlem? medlem = _medlemController.HamtaMedlemById(id);

          
            if (medlem == null)
            {
                MessageBox.Show("Ingen medlem hittades med det ID:t.");
                return;
            }

            
            NamnTextBox.Text = medlem.Namn;
            EpostTextBox.Text = medlem.Epost ?? "";
            TelefonTextBox.Text = medlem.Telefonnummer ?? "";


        }
    }
}
