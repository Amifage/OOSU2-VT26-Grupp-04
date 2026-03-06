using Medlem_Presentationslager.Command;
using Presentationslager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Medlem_Presentationslager.ViewModel
{
    public class StartsidaViewModel
    {
        public ICommand OpenAdminLoginCommand { get; }
        public ICommand OpenMedlemLoginCommand { get; }

        public StartsidaViewModel()
        {
            OpenAdminLoginCommand = new RelayCommand(OpenAdminLogin);
            OpenMedlemLoginCommand = new RelayCommand(OpenMedlemLogin);
        }

        private void OpenAdminLogin(object obj)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            StängFönster(obj);
        }
        private void OpenMedlemLogin(object obj)
        {
            MedlemLogin medlemLogin = new MedlemLogin();
            medlemLogin.Show();
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




       

