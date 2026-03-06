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
        private readonly Medlem inloggadMedlem;
        private readonly ResursController resursController;
        private readonly BokningController bokningController;

        // --- NYA VARIABLER ---
        private DateTime starttid = DateTime.Now;
        private DateTime sluttid = DateTime.Now.AddHours(1);
        private string anteckning;

        public DateTime Starttid
        {
            get => starttid;
            set { starttid = value; OnPropertyChanged(nameof(Starttid)); }
        }

        public DateTime Sluttid
        {
            get => sluttid;
            set { sluttid = value; OnPropertyChanged(nameof(Sluttid)); }
        }

        public string Anteckning
        {
            get => anteckning;
            set { anteckning = value; OnPropertyChanged(nameof(Anteckning)); }
        }

        // --- EXISTERANDE KOD ---
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

        public NyBokningViewModel(Medlem medlem)
        {
            resursController = new ResursController();
            bokningController = new BokningController();

            // Spara ner medlemmen som skickas in
            this.inloggadMedlem = medlem;

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

        private void BekraftaBokning(object obj)
        {
            // SESSIONS-KOLL
            if (inloggadMedlem == null)
            {
                MessageBox.Show("Ingen inloggad användare hittades. Logga in igen.");
                return;
            }

            if (ValdResurs == null)
            {
                MessageBox.Show("Vänligen välj en resurs i listan först!");
                return;
            }

            // TID-VALIDERING
            if (Sluttid <= Starttid)
            {
                MessageBox.Show("Sluttiden måste vara efter starttiden.");
                return;
            }

            try
            {
                Bokning nyBokning = new Bokning
                {
                    //MedlemID = InloggadMedlemSession.AktivMedlem.MedlemID, // döpp om den till inloggad session class namn!
                    MedlemID = inloggadMedlem.MedlemID,
                    ResursID = ValdResurs.ResursID,
                    Starttid = Starttid,
                    Sluttid = Sluttid,
                    SenastUppdaterad = DateTime.Now,
                    Anteckning = string.IsNullOrWhiteSpace(Anteckning) ? null : Anteckning
                };

                bokningController.SkapaBokning(nyBokning);
                MessageBox.Show($"Bokningen av {ValdResurs.Namn} är nu genomförd!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kunde inte skapa bokning: " + ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}