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
    /// Interaction logic for UppdateraResurs.xaml
    /// </summary>
    public partial class UppdateraResurs : Window
    {
        // private readonly MedlemController _medlemController = new MedlemController();
        //private Medlem? medlem;

        private readonly ResursController _resursController = new ResursController();
        private Resurs? resurs;
        public UppdateraResurs()
        {
            InitializeComponent();
        }

        private void SumbitResursIDButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ResursIDTextBox.Text?.Trim(), out int id))
            {
                MessageBox.Show("Skriv ett giltigt resurs-ID (heltal).");
                return;
            }
          
           resurs = _resursController.HamtaResursById(id);

            if (resurs == null)
            {
                MessageBox.Show("Ingen resurs hittades med det ID:t.");
                return;
            }
           
            NamnTextBox.Text = resurs.Namn;
            TypTextBox.Text = resurs.Typ ?? "";
            KapacitetTextBox.Text = resurs.Kapacitet.ToString();

        }

        private void SparaÄndradResursButton_Click(object sender, RoutedEventArgs e)
        {
            if (resurs == null)
            {
                MessageBox.Show("Hämta resursen först");
                return;
            }

            int rows = _resursController.UppdateraResurs(resurs);


            MessageBox.Show(rows == 1 ? "Resurs uppdaterad!" : "Något gick fel");

            resurs.Namn =NamnTextBox.Text.Trim().ToLower();
            resurs.Typ = TypTextBox.Text.Trim().ToLower();
            if (int.TryParse(KapacitetTextBox.Text, out int antal))
            {
                resurs.Kapacitet = antal;
            }
            else
            {
                MessageBox.Show("Vänligen ange kapacitet i siffror");
                return;
            }
            resurs.SenastUppdaterad = DateTime.Now;

        }
    }
}
