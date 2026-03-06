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
    /// Interaction logic for NyBokning.xaml
    /// </summary>
    using Entitetslager;
    using Medlem_Presentationslager.ViewModel;

    public partial class NyBokning : Window
    {
        public NyBokning(Medlem medlem)
        {
            InitializeComponent();
            this.DataContext = new NyBokningViewModel(medlem);
        }
    }
}
