using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Affärslagret;
using Entitetslager;
using Medlem_Presentationslager.Command;

namespace Medlem_Presentationslager.ViewModel
{
    public class NyBokningViewModel : INotifyPropertyChanged
    {

        private readonly ResursController resursController;
        private readonly BokningController bokningController;

        private ObservableCollection<Resurs> tillgangligaResurser;
        public ObservableCollection<Resurs> TillgangligaResurser
        {
            get => tillgangligaResurser;
            set
            {
                tillgangligaResurser = value;
                OnPropertyChanged(nameof(TillgangligaResurser));
            }
        }

        private Resurs valdResurs;
        public Resurs ValdResurs
        {
            get => valdResurs;
            set
            {
                valdResurs = value;
                OnPropertyChanged(nameof(ValdResurs));
            }
        }
        public ICommand BekraftaBokningCommand { get; }

        public NyBokningViewModel()
        {
            resursController = new ResursController();
            bokningController = new BokningController();

            TillgangligaResurser = new ObservableCollection<Resurs>();
            //BekraftaBokningCommand = new RelayCommand(BekraftaBokning);

            LaddaResurser();
        }

        private void LaddaResurser()
        {
            var resurser = resursController.HämtaAllaResurser();

            TillgangligaResurser.Clear();
            foreach (var r in resurser)
            {
                TillgangligaResurser.Add(r);
            }
        }

        //private void BekraftaBokning(object obj)
        //{
        //    if (ValdResurs == null)
        //    {
        //        MessageBox.Show("Vänligen välj en resurs i listan först!");
        //        return;
        //    }

        //    try
        //    {
        //        Bokning nyBokning = new Bokning
        //        {
        //            MedlemID = InloggadMedlemSession.AktivMedlem.MedlemID, // döpp om den till inloggad session class namn!
        //            ResursID = ValdResurs.ResursID,
        //            Starttid = DateTime.Now,
        //            Sluttid = DateTime.Now.AddHours(1),
        //            SenastUppdaterad = DateTime.Now,
        //            Anteckning = "Skapad via medlemssidan"
        //        };

        //        bokningController.SkapaBokning(nyBokning);

        //        MessageBox.Show($"Bokningen av {ValdResurs.Namn} är nu genomförd!");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Kunde inte skapa bokning: " + ex.Message);
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}