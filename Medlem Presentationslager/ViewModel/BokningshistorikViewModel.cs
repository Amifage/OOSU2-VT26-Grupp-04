using Affärslagret;
using Entitetslager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Medlem_Presentationslager.Command;

namespace Medlem_Presentationslager.ViewModel
{
    public class BokningshistorikViewModel : INotifyPropertyChanged
    {
        private readonly BokningController _bokningController;
        private readonly Medlem _inloggadMedlem;

        public ObservableCollection<Bokning> Bokningshistorik { get; set; }

        public ICommand TillbakaCommand { get; }

        public BokningshistorikViewModel(Medlem medlem)
        {
            _bokningController = new BokningController();
            _inloggadMedlem = medlem;

            Bokningshistorik = new ObservableCollection<Bokning>();

            LaddaBokningshistorik();

            TillbakaCommand = new RelayCommand(Tillbaka);
        }

        private void LaddaBokningshistorik()
        {
            try
            {
                var allaBokningar = _bokningController
                    .HämtaBokningarFörMedlem(_inloggadMedlem.MedlemID)
                    .OrderByDescending(b => b.Starttid)
                    .ToList();

                Bokningshistorik.Clear();

                foreach (var bokning in allaBokningar)
                {
                    Bokningshistorik.Add(bokning);
                }

                MessageBox.Show($"Antal bokningar hittade: {allaBokningar.Count}");
                OnPropertyChanged(nameof(Bokningshistorik));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kunde inte hämta bokningshistorik: " + ex.Message);
            }
        }
        }

        private void Tillbaka(object obj)
        {
            MenyMedlem meny = new MenyMedlem(_inloggadMedlem);
            meny.Show();

            if (obj is Window window)
                window.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string namn = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(namn));
        }
    }
}
