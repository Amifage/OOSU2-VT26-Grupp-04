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
        public UppdateraMedlem()
        {
            InitializeComponent();
        }

        private void SumbitMedlemIDButton_Click(object sender, RoutedEventArgs e)
        {
            ÄndraMedlem ÄndraMedlemFönster = new ÄndraMedlem();
            ÄndraMedlemFönster.Show();
                
        }
    }
}
