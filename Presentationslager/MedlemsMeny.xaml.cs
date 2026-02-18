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
    /// Interaction logic for MedlemsMeny.xaml
    /// </summary>
    public partial class MedlemsMeny : Window
    {
        public MedlemsMeny()
        {
            InitializeComponent();
        }
        private void RegistreraMedlemButton_Click(object sender, RoutedEventArgs e)
        {
            RegistreraMedlem registreraMedlemfönster = new RegistreraMedlem();
            registreraMedlemfönster.Show();
        }
        private void UppdateraMedlemButton_Click(object sender, RoutedEventArgs e)
        {
            UppdateraMedlem HämtaMedlemfönster = new UppdateraMedlem();
            HämtaMedlemfönster.Show();
        }

        private void MedlemsStatistikButton_Click(object sender, RoutedEventArgs e)
        {
            MedlemsAktivitet medlemsAktivitet = new MedlemsAktivitet();
            medlemsAktivitet.Show();
            this.Close();
        }
    }
}
