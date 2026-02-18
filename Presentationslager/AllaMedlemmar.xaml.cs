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
    /// Interaction logic for AllaMedlemmar.xaml
    /// </summary>
    public partial class AllaMedlemmar : Window
    {
        private readonly MedlemController _medlemController = new MedlemController();
        public AllaMedlemmar()
        {
            InitializeComponent();
            LaddaMedlemmar();
        }
        private void LaddaMedlemmar()
        {
            MedlemmarDataGrid.ItemsSource = _medlemController.HämtaAllaMedlemmar();
        }
    }
}
