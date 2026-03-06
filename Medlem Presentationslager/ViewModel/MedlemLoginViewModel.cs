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

        private string _email;
        private string _lösenord;
        private string _felmeddelande;

        private readonly MedlemController _medlemController;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
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



        public ICommand OpenSkapaMedlemCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand TillbakaCommand { get; }

        public MedlemLoginViewModel()
        {
            _medlemController = new MedlemController();
            LoginCommand = new RelayCommand(UtförLogin);
            OpenSkapaMedlemCommand = new RelayCommand(OpenSkapaMedlem);
            TillbakaCommand = new RelayCommand(Tillbaka);
        }

        private void OpenSkapaMedlem(object obj)
        {
            SkapaMedlem skapaMedlem = new SkapaMedlem();
            skapaMedlem.Show();
            StängFönster(obj);
        }

        private void Tillbaka(object obj)
        {
            Startsida startsida = new Startsida();
            startsida.Show();
            StängFönster(obj);
        }

        private void StängFönster(object obj)
        {
            if (obj is Window fönster)
            {
                fönster.Close();
            }
        }

        private void UtförLogin(object obj)
        {
            string epost = Email?.Trim().ToLower() ?? "";
            string losenord = Lösenord?.Trim().ToLower() ?? "";

            var medlem = _medlemController.ValideraInloggningEpost(epost, losenord);

            if (medlem != null)
            {
                Felmeddelande = "";
                // SKICKA MED MEDLEMMEN HÄR
                MenyMedlem menyMedlem = new MenyMedlem(medlem);
                menyMedlem.Show();
                StängFönster(obj);
            }
            else
            {
                MessageBox.Show("Fel e-postadress eller lösenord", "Inloggning misslyckades", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
