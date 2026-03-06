using Medlem_Presentationslager.ViewModel;
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

namespace Medlem_Presentationslager
{
    /// <summary>
    /// Interaction logic for MedlemLogin.xaml
    /// </summary>
    public partial class MedlemLogin : Window
    {
        public MedlemLogin()
        {
            InitializeComponent();
            DataContext = new MedlemLoginViewModel(); //Denna beövs för att WPF ska veta var den ska hämta/läsa från.
        }

        private void LosenordBox_PasswordChanged(object sender, RoutedEventArgs e) //Passwordbox är inte en DependencyProperty så därför fungerar inte binding. Av säkerhetsskäl behöver vi göra på detta sättet istället.
        {
            if (DataContext is MedlemLoginViewModel vm && sender is PasswordBox passwordBox)
            {
                vm.Lösenord = passwordBox.Password;
            }
        }
    }
}
