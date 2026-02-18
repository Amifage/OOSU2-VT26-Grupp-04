using Affärslagret;
using Entitetslager;
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
    /// Interaction logic for KommandeBokningar.xaml
    /// </summary>
    public partial class KommandeBokningar : Window
        
    {
        private BokningController bokningController;
        public KommandeBokningar()
        {
            InitializeComponent();
            bokningController=new BokningController();                 
            LaddaBokningar();

        }
        private void LaddaBokningar()
        {
            var list = bokningController.HämtaKommandeBokningar();
            if (list != null)
            {
                BokningsDataGrid.ItemsSource = list;
            }

          
        }
    }
}
