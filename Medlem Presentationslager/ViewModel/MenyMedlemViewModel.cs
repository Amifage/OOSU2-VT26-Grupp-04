using Medlem_Presentationslager.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace Medlem_Presentationslager.ViewModel
{
    public class MenyMedlemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SkapaBokningCommand { get; set; }
        public ICommand MinaSidorCommand { get; set; }
        public ICommand AndraAvbokaCommand { get; set; }
        public ICommand BokningshistorikCommand { get; set; }
        public ICommand LoggaUtCommand { get; set; }
        public ICommand MedlemarCommand { get; set; }

        public MenyMedlemViewModel()
        {
            SkapaBokningCommand = new RelayCommand(OpenSkapaBokning);
            MinaSidorCommand = new RelayCommand(OpenMinaSidor);
            AndraAvbokaCommand = new RelayCommand(OpenAndraAvboka);
            BokningshistorikCommand = new RelayCommand(OpenBokningshistorik);
            LoggaUtCommand = new RelayCommand(LoggaUt);
            MedlemarCommand = new RelayCommand(OpenMedlemar);

        }

        private void OpenMedlemar()
        {
            Medlemmar window = new Medlemmar();
            window.Show();
            //StangAktivtFonster();
        }
        private void OpenSkapaBokning()
        {
            SkapaMedlem window = new SkapaMedlem();
            window.Show();

            //StangAktivtFonster();
        }

        private void OpenMinaSidor()
        {
            MinaSidor window = new MinaSidor();
            window.Show();

            //StangAktivtFonster();
        }

        private void OpenAndraAvboka()
        {
            UppdateraBokning window = new UppdateraBokning();
            window.Show();

            //StangAktivtFonster();
        }

        private void OpenBokningshistorik()
        {
            Historik window = new Historik();
            window.Show();

            //StangAktivtFonster();
        }

        private void LoggaUt()
        {
            MedlemLogin window = new MedlemLogin();
            window.Show();

            //StangAktivtFonster();
        }

        
        }
    
    }

