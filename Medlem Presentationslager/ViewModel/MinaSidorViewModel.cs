
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
        private string _medlemsNivå;
        private string _nyttLösenord;

       

        public string NyttLösenord
        {
            get => _nyttLösenord;
            set { _nyttLösenord = value; OnPropertyChanged(); }
        }

        public Medlem InloggadMedlem
            {
                get => _inloggadMedlem;
                set { _inloggadMedlem = value; OnPropertyChanged(); }
            }
        public List<string> MedlemsNivåer { get; } = new List<string>
        {
            "Företag",
            "Fast",
            "Flex"
        };
       
        public string MedlemsNivå
        {
            get => _medlemsNivå;
            set
            {
                _medlemsNivå = value;
                // VIKTIGT: Uppdatera objektet som faktiskt sparas i databasen
                if (InloggadMedlem != null)
                {
                    InloggadMedlem.Medlemsnivå = value;
                }
                OnPropertyChanged();
            }
        }

        public ICommand SparaÄndringarCommand { get; }
            public ICommand TillbakaCommand { get; }

        public MinaSidorViewModel(Medlem medlem)
        {
            _medlemController = new MedlemController();
            InloggadMedlem = medlem;

            // Sätt utgångsvärdet för ComboBoxen baserat på medlemmens nuvarande status
            MedlemsNivå = medlem.Medlemsnivå;

            // Kommandon
            SparaÄndringarCommand = new RelayCommand(SparaÄndringar);
            TillbakaCommand = new RelayCommand(Tillbaka);
        }

        private void SparaÄndringar(object obj)
        {
            try
            {
                // 1. Hantera E-post (Säkerställ små bokstäver)
                if (!string.IsNullOrEmpty(InloggadMedlem.Epost))
                {
                    InloggadMedlem.Epost = InloggadMedlem.Epost.Trim().ToLower();
                }

                // 2. Hantera nytt lösenord från TextBox-bindningen
                if (!string.IsNullOrWhiteSpace(NyttLösenord))
                {
                    // Uppdatera medlemmens lösenord och tvinga till små bokstäver för inloggningsmatchning
                    InloggadMedlem.Lösenord = NyttLösenord.Trim().ToLower();

                    // Rensa fältet i gränssnittet efter lyckad tilldelning
                    NyttLösenord = string.Empty;
                }

         

                // 3. Spara till databas via controllern
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
            // Nu kan vi skicka tillbaka rätt medlem till menyn!
            MenyMedlem meny = new MenyMedlem(_inloggadMedlem);
            meny.Show();
            StängFönster(obj);
        }

        private void StängFönster(object parameter)
        {
            if (parameter is Window fönster)
            {
                fönster.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = null) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    
}
    

