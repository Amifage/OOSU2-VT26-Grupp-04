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
    /// Interaction logic for BokningsMeny.xaml
    /// </summary>
    public partial class BokningsMeny : Window
    {
        public BokningsMeny()
        {
            InitializeComponent();
        }
        private void SkapaBokningButton_Click(object sender, RoutedEventArgs e)
        {
            SkapaBokning skapaBokningfönster = new SkapaBokning();
            skapaBokningfönster.Show();
        }

        private void HanteraBokningButton_Click(object sender, RoutedEventArgs e)
        {
            HanteraBokning HanteraBokningfönster = new HanteraBokning();
                HanteraBokningfönster.Show();
        }
        private void KommandeBoknigarButton_Click(object sender, RoutedEventArgs e)
        {
            KommandeBokningar kommandeBoknigar = new KommandeBokningar();
                kommandeBoknigar.Show();
        }
    }
}
