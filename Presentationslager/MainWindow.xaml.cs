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
            personalController = new PersonalController();

        }

        private void LoggaInButton_Click(object sender, RoutedEventArgs e)
        {

            string namn = EmailTextBox.Text.Trim();
            string lösenord = LösenordTextBox.Password.Trim().ToLower();

            var personal = personalController.ValideraInloggning(namn, lösenord);

            if (personal != null)
            {
                PersonalMeny personalMeny= new PersonalMeny();
                
                personalMeny.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Fel namn eller lösenord");

            }
        }

            
        }
    }
