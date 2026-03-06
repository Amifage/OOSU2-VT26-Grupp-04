
           using System;
            using System.Windows;
            using System.Windows.Input;
            using System.Windows.Media.Imaging;
            using Microsoft.Win32;
            using Entitetslager;
            using Affärslagret;
            using Medlem_Presentationslager.Command;
            using System.ComponentModel;
            using System.Runtime.CompilerServices;

namespace Medlem_Presentationslager.ViewModel
{
        public class MinaSidorViewModel : INotifyPropertyChanged
        {
            private readonly MedlemController _medlemController;
            private Medlem _inloggadMedlem;

            public Medlem InloggadMedlem
            {
                get => _inloggadMedlem;
                set { _inloggadMedlem = value; OnPropertyChanged(); }
            }


            public ICommand SparaÄndringarCommand { get; }
            public ICommand TillbakaCommand { get; }

            public MinaSidorViewModel(Medlem medlem)
            {
                _medlemController = new MedlemController();
                InloggadMedlem = medlem;

                // Kommandon
                SparaÄndringarCommand = new RelayCommand(SparaÄndringar);
                TillbakaCommand = new RelayCommand(Tillbaka);

            }

            private void SparaÄndringar(object obj)
            {
                try
                {
                    _medlemController.UppdateraMedlem(InloggadMedlem);
                    MessageBox.Show("Dina uppgifter har uppdaterats!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kunde inte spara: " + ex.Message);
                }
            }

            private void Tillbaka(object obj)
            {
                if (obj is Window window) window.Close();
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = null) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    
}
    

