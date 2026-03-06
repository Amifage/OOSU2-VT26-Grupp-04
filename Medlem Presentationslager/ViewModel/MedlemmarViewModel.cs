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

        public MedlemmarViewModel()
        {
            _medlemController = new MedlemController();
            LaddaMedlemmar();
        }

        private void LaddaMedlemmar()
        {
            // Hämtar alla medlemmar från affärslagret och lägger i kollektionen
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
