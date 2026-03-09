using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
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
        private string _valdBonnivå;
        private string _nyttLösenord;

        public Medlem InloggadMedlem
        {
            get => _inloggadMedlem;
            set { _inloggadMedlem = value; OnPropertyChanged(); }
        }

        public List<string> MedlemsNivåer { get; } = new List<string> { "Företag", "Fast", "Flex" };

        public string MedlemsNivå 
        {
            get => _valdBonnivå;
            set
            {
                _valdBonnivå = value;
                if (InloggadMedlem != null)
                {

                    InloggadMedlem.Medlemsnivå = value;
                }
                OnPropertyChanged();
            }
        }

        public string NyttLösenord
        {
            get => _nyttLösenord;
            set { _nyttLösenord = value; OnPropertyChanged(); }
        }

        public ICommand SparaÄndringarCommand { get; }
        public ICommand TillbakaCommand { get; }

        public MinaSidorViewModel(Medlem medlem)
        {
            _medlemController = new MedlemController();
            //InloggadMedlem = medlem;
            InloggadMedlem = _medlemController.HämtaMedlemById(medlem.MedlemID); //Ovan är ändrad till detta för att uppdatera medlemspoäng.

            // Viktigt: Sätt startvärdet så ComboBoxen visar rätt nivå direkt
            MedlemsNivå = medlem.Medlemsnivå;

            SparaÄndringarCommand = new RelayCommand(SparaÄndringar);
            TillbakaCommand = new RelayCommand(Tillbaka);
        }

        private void SparaÄndringar(object obj)
        {
            try
            {
                // Formatera e-post
                if (!string.IsNullOrEmpty(InloggadMedlem.Epost))
                    InloggadMedlem.Epost = InloggadMedlem.Epost.Trim().ToLower();

                // Hantera nytt lösenord
                if (!string.IsNullOrWhiteSpace(NyttLösenord))
                {
                    InloggadMedlem.Lösenord = NyttLösenord.Trim().ToLower();
                    NyttLösenord = string.Empty;
                }

                // Spara objektet där Medlemsnivå nu är korrekt satt
                _medlemController.UppdateraMedlem(InloggadMedlem);
                MessageBox.Show("Dina uppgifter har uppdaterats!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kunde inte spara: " + ex.InnerException?.Message);
            }
        }

        private void Tillbaka(object obj)
        {
            MenyMedlem meny = new MenyMedlem(_inloggadMedlem);
            meny.Show();
            if (obj is Window window) window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}