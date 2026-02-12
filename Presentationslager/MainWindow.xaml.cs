using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Affärslagret;
using Entitetslager;

namespace Presentationslager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new DatabasController();

        }

        private void LoggaInButton_Click(object sender, RoutedEventArgs e)
        {
            PersonalMeny personalMeny = new PersonalMeny();
            personalMeny.Show();
            //_medlemController.SkapaMedlem(nyMedlem);
            //_personalController.Validera
            this.Close();
        }
    }
}