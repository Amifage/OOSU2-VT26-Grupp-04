using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Affärslagret;
using Entitetslager;
using System.Windows;
using Medlem_Presentationslager.Command;
using System.Windows.Input;
using Presentationslager;



namespace Medlem_Presentationslager.ViewModel
{
    public class SkapaMedlemViewModel 
    {
        public SkapaMedlemViewModel() 
        {
            TillbakaCommand = new RelayCommand(Tillbaka);

        }
        public ICommand TillbakaCommand { get; }
        private void Tillbaka(object obj)
        {
         
            MedlemLogin login = new MedlemLogin();
            login.Show();

     
            StängFönster(obj);
        }
        private void StängFönster(object parameter)
        {
            if (parameter is Window fönster)
            {
                fönster.Close();
            }
        }
    }
}
