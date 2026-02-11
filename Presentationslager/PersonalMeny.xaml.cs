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
    /// Interaction logic for PersonalMeny.xaml
    /// </summary>
    public partial class PersonalMeny : Window
    {
        public PersonalMeny()
        {
            InitializeComponent();
        }
        private void SkapaBokningButton_Click(object sender, RoutedEventArgs e)
        {
            SkapaBokning skapaBokningfönster = new SkapaBokning();
            skapaBokningfönster.Show();
        }

        private void RegistreraMedlemButton_Click(object sender, RoutedEventArgs e)
        {
            RegistreraMedlem registreraMedlemfönster = new RegistreraMedlem();
            registreraMedlemfönster.Show();
        }

        private void RegistreraResursButton_Click(object sender, RoutedEventArgs e)
        {
            RegistreraResurs registreraResursfönster = new RegistreraResurs();
            registreraResursfönster.Show();
        }

        private void UppdateraResursButton_Click(object sender, RoutedEventArgs e)
        {
            UppdateraResurs UppdateraResursfönster = new UppdateraResurs();
            UppdateraResursfönster.Show();
        }

        private void UppdateraMedlemButton_Click(object sender, RoutedEventArgs e)
        {
            UppdateraMedlem HämtaMedlemfönster = new UppdateraMedlem();
            HämtaMedlemfönster.Show();
        }

        private void HanteraBokningButton_Click(object sender, RoutedEventArgs e)
        {
            HanteraBokning HanteraBokningfönster = new HanteraBokning();
            HanteraBokningfönster.Show();
        }

        private void VisaStatistikButton_Click(object sender, RoutedEventArgs e)
        {
            VisaStatisik VisaStatistikFönster = new VisaStatisik();
            VisaStatistikFönster.Show();
        }

        private void VisaTillgänglighetButton_Click(object sender, RoutedEventArgs e)
        {
            VisaTillgänglighet VisaTillgänglighetFönster = new VisaTillgänglighet();
            VisaTillgänglighetFönster.Show();
        }
    }
}
