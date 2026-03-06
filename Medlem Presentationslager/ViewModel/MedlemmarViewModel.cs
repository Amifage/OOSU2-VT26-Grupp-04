using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Affärslagret;
using Entitetslager;
using System.Windows;
using Medlem_Presentationslager.Command;
using System.Windows.Input;
using Presentationslager;

namespace Medlem_Presentationslager.ViewModel
{
    public class MedlemmarViewModel : INotifyPropertyChanged
    {
        private readonly MedlemController _medlemController;
        private ObservableCollection<Medlem> _medlemmar;
        private Medlem _inloggadMedlem;

        public ObservableCollection<Medlem> Medlemmar
        {
            get => _medlemmar;
            set
            {
                _medlemmar = value;
                OnPropertyChanged();
            }
        }
        public ICommand TillbakaCommand { get; }
        public MedlemmarViewModel(Medlem inloggad) // Ta emot vid start
        {
            _medlemController = new MedlemController();
            _inloggadMedlem = inloggad;

            LaddaMedlemmar();
            TillbakaCommand = new RelayCommand(Tillbaka);
        }

        private void Tillbaka(object obj)
        {
            // Nu kan vi skicka tillbaka rätt medlem till menyn!
            MenyMedlem meny = new MenyMedlem(_inloggadMedlem);
            meny.Show();
            StängFönster(obj);
        }

        public MedlemmarViewModel()
        {
            _medlemController = new MedlemController();
            LaddaMedlemmar();
            TillbakaCommand = new RelayCommand(Tillbaka);
        }

        private void LaddaMedlemmar()
        {       
            var lista = _medlemController.HämtaAllaMedlemmar();
            Medlemmar = new ObservableCollection<Medlem>(lista);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void StängFönster(object parameter)
        {
            if (parameter is Window fönster)
            {
                fönster.Close();
            }
        }
    }
}
