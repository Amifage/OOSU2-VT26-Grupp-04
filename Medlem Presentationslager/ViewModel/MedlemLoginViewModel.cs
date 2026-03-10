using Affärslagret;
using Medlem_Presentationslager.Command;
using Presentationslager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Medlem_Presentationslager.ViewModel
{
    public class MedlemLoginViewModel : INotifyPropertyChanged
    {

        private string _epost;
        private string _lösenord;
        private string _felmeddelande;

        private readonly MedlemController _medlemController;

        #region Properties 
                             //Properties behövs för att hantera input från användaren
        public string Epost
        {
            get => _epost;
            set
            {
                _epost = value;
                OnPropertyChanged();
            }
        }

        public string Lösenord
        {
            get => _lösenord;
            set
            {
                _lösenord = value;
                OnPropertyChanged();
            }
        }

        public string Felmeddelande
        {
            get => _felmeddelande;
            set
            {
                _felmeddelande = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) //Uppdaterar variabler
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public ICommand OpenSkapaMedlemCommand { get; } //Kopplar knapptryck
        public ICommand LoginCommand { get; }
        public ICommand TillbakaCommand { get; }

        public MedlemLoginViewModel()
        {
            _medlemController = new MedlemController();
            LoginCommand = new RelayCommand(UtförLogin);
            OpenSkapaMedlemCommand = new RelayCommand(OpenSkapaMedlem);
            TillbakaCommand = new RelayCommand(Tillbaka);
        }

        #region Metoder
        private void OpenSkapaMedlem(object obj) //Tar ett objekt, i detta fallet ett fönster. Hade lika väl kunnat heta parameter.
        {
            SkapaMedlem skapaMedlem = new SkapaMedlem();
            skapaMedlem.Show();
            StängFönster(obj);
        }

        private void UtförLogin(object obj)
        {
            string epost = Epost?.Trim().ToLower() ?? "";
            string lösenord = Lösenord?.Trim().ToLower() ?? "";

            var medlem = _medlemController.ValideraInloggningEpost(epost, lösenord);

            if (medlem != null)
            {
                Felmeddelande = "";
                MenyMedlem menyMedlem = new MenyMedlem(medlem); //Skickar med medlem
                menyMedlem.Show();
                StängFönster(obj);
            }
            else
            {
                MessageBox.Show("Fel e-postadress eller lösenord", "Inloggning misslyckades", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Tillbaka/Stäng metoder
        private void Tillbaka(object obj) //Tar ett objekt, i detta fallet ett fönster.
        {
            Startsida startsida = new Startsida();
            startsida.Show();
            StängFönster(obj);
        }

        private void StängFönster(object obj) //Tar ett objekt, i detta fallet ett fönster.
        {
            if (obj is Window fönster)
            {
                fönster.Close();
            }
        }
        #endregion
    }

}
