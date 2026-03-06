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
        private void Tillbaka(object obj)
        {
            // 1. Öppna MedlemsMeny
            MenyMedlem meny = new MenyMedlem();
            meny.Show();      

            // Stäng meny-fönstret
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
