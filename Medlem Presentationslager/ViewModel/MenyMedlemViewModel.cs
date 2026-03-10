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
        public ICommand ÄndraAvbokaCommand { get; set; }
        public ICommand BokningshistorikCommand { get; set; }
        public ICommand LoggaUtCommand { get; set; }
        public ICommand MedlemarCommand { get; set; }

        private Medlem _inloggadMedlem;

        public MenyMedlemViewModel(Medlem medlem)
        {
            _inloggadMedlem = medlem;
            SkapaBokningCommand = new RelayCommand(OpenSkapaBokning);
            MinaSidorCommand = new RelayCommand(OpenMinaSidor);
            ÄndraAvbokaCommand = new RelayCommand(OpenÄndraAvboka);
            BokningshistorikCommand = new RelayCommand(OpenBokningshistorik);
            LoggaUtCommand = new RelayCommand(LoggaUt);
            MedlemarCommand = new RelayCommand(OpenMedlemar);
        }

        #region Metoder
        private void OpenMedlemar(object obj) //Vi ser till att alla metoder tar emot ett 'obj' (vilket kommer vara fönstret).
        {
            Medlemmar window = new Medlemmar(_inloggadMedlem);
            window.Show();
            StängFönster(obj);
        }

        private void OpenSkapaBokning(object obj)
        {          
            NyBokning window = new NyBokning(_inloggadMedlem); //Skickar med den sparade medlemen till mina sidor.
            window.Show();
            StängFönster(obj);
        }

        private void OpenMinaSidor(object obj)
        {
            MinaSidor window = new MinaSidor(_inloggadMedlem);
            window.Show();
            StängFönster(obj);
        }

        private void OpenÄndraAvboka(object obj)
        {
            UppdateraBokning window = new UppdateraBokning(_inloggadMedlem);
            window.Show();
            StängFönster(obj);
        }

        private void OpenBokningshistorik(object obj)
        {
            Historik window = new Historik(_inloggadMedlem);
            window.Show();
            StängFönster(obj);
        }
        #endregion

        #region LoggaUt/Stäng metod
        private void LoggaUt(object obj)
        {
            // Öppna login-fönstret
            MedlemLogin window = new MedlemLogin();
            window.Show();

            // Stäng meny-fönstret
            StängFönster(obj);
        }

        // En hjälpmetod som stänger fönstret som skickas med från XAML
        private void StängFönster(object parameter)
        {
            if (parameter is Window fönster)
            {
                fönster.Close();
            }
        }
        #endregion
    }
}