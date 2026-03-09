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
using Entitetslager;

namespace Medlem_Presentationslager
{
    /// <summary>
    /// Interaction logic for Medlemmar.xaml
    /// </summary>
    public partial class Medlemmar : Window
    {
        public Medlemmar(Medlem medlem)
        {
            InitializeComponent();
            DataContext = new MedlemmarViewModel(medlem);
        }
    }
}
