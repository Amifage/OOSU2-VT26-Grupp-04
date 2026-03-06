using Affärslagret;
using Medlem_Presentationslager.Command;
using Presentationslager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public ICommand LoginCommand { get; }

        public MedlemLoginViewModel()
        {
            _medlemController = new MedlemController();
            LoginCommand = new RelayCommand(UtförLogin);
        }

        private void UtförLogin()
        {
          
            string epost = Email?.Trim().ToLower() ?? "";
            string losenord = Lösenord?.Trim().ToLower() ?? "";

            var medlem = _medlemController.ValideraInloggningEpost(epost, losenord);

            if (medlem != null)
            {
                Felmeddelande = "";

                MenyMedlem menyMedlem = new MenyMedlem();
                menyMedlem.Show();
            }
            else
            {
                Felmeddelande = "Fel e-postadress eller lösenord";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
