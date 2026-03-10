using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Entitetslager;
using Affärslagret;
using Medlem_Presentationslager.Command;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;

namespace Medlem_Presentationslager.ViewModel
{
    public class MinaSidorViewModel : INotifyPropertyChanged
    {
        private readonly MedlemController _medlemController;
        private Medlem _inloggadMedlem;
        private string _valdBonnivå;
        private string _nyttLösenord;

        #region Properties
        public Medlem InloggadMedlem
        {
            get => _inloggadMedlem;
            set { _inloggadMedlem = value; OnPropertyChanged(); }
        }

        public List<string> MedlemsNivåer { get; } = new List<string> { "Företag", "Fast", "Flex" }; //Används i WPF fösntret för att visa medlemsnivåer i combobox.

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
        #endregion

        public ICommand SparaÄndringarCommand { get; }
        public ICommand TillbakaCommand { get; }
        public ICommand VäljBildCommand { get; }

        public MinaSidorViewModel(Medlem medlem)
        {
            _medlemController = new MedlemController();
            InloggadMedlem = _medlemController.HämtaMedlemById(medlem.MedlemID); //Metoden uppdaterar medlemspoäng.

            MedlemsNivå = medlem.Medlemsnivå;

            SparaÄndringarCommand = new RelayCommand(SparaÄndringar);
            TillbakaCommand = new RelayCommand(Tillbaka);
            VäljBildCommand = new RelayCommand(VäljBild);
        }

        #region Metoder

        private void VäljBild(object obj)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Bilder (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Läs in filen som byte-array
                    byte[] bildBytes = System.IO.File.ReadAllBytes(openFileDialog.FileName);

                    // Uppdatera medlemsobjektet
                    InloggadMedlem.Profilbild = bildBytes;

                    // Meddela UI att bilden har ändrats
                    OnPropertyChanged(nameof(InloggadMedlem));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kunde inte ladda bilden: " + ex.Message);
                }
            }
        }
        private void SparaÄndringar(object obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(InloggadMedlem.Epost))
                    InloggadMedlem.Epost = InloggadMedlem.Epost.Trim().ToLower();

                if (!string.IsNullOrWhiteSpace(NyttLösenord))
                {
                    InloggadMedlem.Lösenord = NyttLösenord.Trim().ToLower();
                    NyttLösenord = string.Empty;
                }

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
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}