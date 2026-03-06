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
        private void ExecuteTillbaka(object parameter)
        {
            // 1. Öppna MedlemsMeny
            MedlemsMeny meny = new MedlemsMeny();
            meny.Show();

            // 2. Stäng nuvarande fönster (skickas med som parameter från XAML)
            if (parameter is Window nuvarandeFönster)
            {
                nuvarandeFönster.Close();
            }
        }

        public MedlemmarViewModel()
        {
            _medlemController = new MedlemController();
            LaddaMedlemmar();
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
    }
}
