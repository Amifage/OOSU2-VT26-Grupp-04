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
    /// Interaction logic for ResursMeny.xaml
    /// </summary>
    public partial class ResursMeny : Window
    {
        public ResursMeny()
        {
            InitializeComponent();
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
        private void VisaTillgänglighetButton_Click(object sender, RoutedEventArgs e)
        {
            VisaTillgänglighet VisaTillgänglighetFönster = new VisaTillgänglighet();
            VisaTillgänglighetFönster.Show();
        }       

        private void AllaResurserButton_Click(object sender, RoutedEventArgs e)
        {
            AllaResurser allaResurser = new AllaResurser();
            allaResurser.Show();
        }
    }
}
