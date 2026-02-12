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
        private readonly PersonalController personalController = new PersonalController();
        public MainWindow()
        {
            InitializeComponent();
            new DatabasController();

        }

        private void LoggaInButton_Click(object sender, RoutedEventArgs e)
        {
            PersonalMeny personalMeny = new PersonalMeny();
            personalMeny.Show();


            string namn = EmailTextBox.Text.Trim().ToLower();
            string lösenord = LösenordTextBox.Text.Trim().ToLower();

            var inloggning = new Personal
            {
                Namn = namn,
                Lösenord = lösenord
            }

            personalController.ValideraInloggning(inloggning);
           
          

           

            this.Close();
        }
    }
}