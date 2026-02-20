using Affärslagret;
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
    /// Interaction logic for AllaResurser.xaml
    /// </summary>
    public partial class AllaResurser : Window
    {
        private readonly ResursController _resursController = new ResursController();
        public AllaResurser()
        {
            InitializeComponent();
            LaddaResurser();
        }
        private void LaddaResurser()
        {
            ResurserDataGrid.ItemsSource = _resursController.HämtaAllaResurser();
        }
    }
}
