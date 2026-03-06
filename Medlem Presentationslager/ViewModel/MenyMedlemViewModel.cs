using Affärslagret;
using Entitetslager;
using Medlem_Presentationslager.Command; 
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
            // Vi ser till att alla metoder tar emot ett 'obj' (vilket kommer vara fönstret)
            SkapaBokningCommand = new RelayCommand(OpenSkapaBokning);
            MinaSidorCommand = new RelayCommand(OpenMinaSidor);
            AndraAvbokaCommand = new RelayCommand(OpenAndraAvboka);
            BokningshistorikCommand = new RelayCommand(OpenBokningshistorik);
            LoggaUtCommand = new RelayCommand(LoggaUt);
            MedlemarCommand = new RelayCommand(OpenMedlemar);
        }

        private void OpenMedlemar(object obj)
        {
            Medlemmar window = new Medlemmar();
            window.Show();
            StangFonster(obj);
        }

        private void OpenSkapaBokning(object obj)
        {
            // Notera: Du hade SkapaMedlem här, dubbelkolla om det ska vara NyBokning?
            SkapaMedlem window = new SkapaMedlem();
            window.Show();
            StangFonster(obj);
        }

        private void OpenMinaSidor(object obj)
        {
            MinaSidor window = new MinaSidor();
            window.Show();
            StangFonster(obj);
        }

        private void OpenAndraAvboka(object obj)
        {
            UppdateraBokning window = new UppdateraBokning();
            window.Show();
            StangFonster(obj);
        }

        private void OpenBokningshistorik(object obj)
        {
            Historik window = new Historik();
            window.Show();
            StangFonster(obj);
        }

        private void LoggaUt(object obj)
        {
            // Öppna login-fönstret
            MedlemLogin window = new MedlemLogin();
            window.Show();

            // Stäng meny-fönstret
            StangFonster(obj);
        }

        // En hjälpmetod som stänger fönstret som skickas med från XAML
        private void StangFonster(object parameter)
        {
            if (parameter is Window fönster)
            {
                fönster.Close();
            }
        }
    }
}