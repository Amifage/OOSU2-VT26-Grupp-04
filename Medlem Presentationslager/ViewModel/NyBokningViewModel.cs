using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        private DateTime valdDatum = DateTime.Today;
        public DateTime ValdDatum
        {
            get => valdDatum;
            set { valdDatum = value; UppdateraProperty(nameof(ValdDatum)); }
        }

        private string valdStarttid;
        public string ValdStarttid
        {
            get => valdStarttid;
            set { valdStarttid = value; UppdateraProperty(nameof(ValdStarttid)); }
        }

        private string valdSluttid;
        public string ValdSluttid
        {
            get => valdSluttid;
            set { valdSluttid = value; UppdateraProperty(nameof(ValdSluttid)); }
        }

        public ObservableCollection<string> Starttider { get; } = GeneraTider();
        public ObservableCollection<string> Sluttider { get; } = GeneraTider();

        private static ObservableCollection<string> GeneraTider()
        {
            var tider = new ObservableCollection<string>();
            for (int h = 6; h <= 22; h++)
            {
                tider.Add($"{h:00}:00");
                tider.Add($"{h:00}:30");
            }
            return tider;
        }

        private string anteckning;
        public string Anteckning
        {
            get => anteckning;
            set { anteckning = value; UppdateraProperty(nameof(Anteckning)); }
        }
        private ObservableCollection<Resurs> tillgangligaResurser = new ObservableCollection<Resurs>();


        public ObservableCollection<Resurs> TillgangligaResurser
        {
            get => tillgangligaResurser;
            set { tillgangligaResurser = value; UppdateraProperty(nameof(TillgangligaResurser)); }
        }

        private Resurs valdResurs;
        public Resurs ValdResurs
        {
            get => valdResurs;
            set { valdResurs = value; UppdateraProperty(nameof(ValdResurs)); }
        }

        public ICommand HamtaLedigaResurserCommand { get; }
        public ICommand BekraftaBokningCommand { get; }
        public ICommand TillbakaCommand { get; }

        public NyBokningViewModel(Medlem medlem)
        {
            resursController = new ResursController();
            bokningController = new BokningController();
            inloggadMedlem = medlem;

            HamtaLedigaResurserCommand = new RelayCommand(HamtaLedigaResurser);
            BekraftaBokningCommand = new RelayCommand(BekraftaBokning);
            TillbakaCommand = new RelayCommand(_ =>
            {
                Application.Current.Windows
                    .OfType<Window>()
                    .FirstOrDefault(w => w.IsActive)
                    ?.Close();
            });
        }

        private void HamtaLedigaResurser(object obj)
        {
            if (string.IsNullOrEmpty(ValdStarttid) || string.IsNullOrEmpty(ValdSluttid))
            {
                MessageBox.Show("Välj starttid och sluttid först.");
                return;
            }

            var starttid = ByggDateTime(ValdStarttid);
            var sluttid = ByggDateTime(ValdSluttid);

            if (sluttid <= starttid)
            {
                MessageBox.Show("Sluttiden måste vara efter starttiden.");
                return;
            }

            try
            {
                var alla = resursController.HämtaAllaResurser();
                var bokadeResursIds = bokningController.HämtaKommandeBokningar()
                    .Where(b => b.Starttid < sluttid && b.Sluttid > starttid)
                    .Select(b => b.ResursID)
                    .ToHashSet();

                TillgangligaResurser.Clear();
                foreach (var r in alla.Where(r => !bokadeResursIds.Contains(r.ResursID)))
                    TillgangligaResurser.Add(r);

                if (!TillgangligaResurser.Any())
                    MessageBox.Show("Inga lediga resurser för vald tid.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel vid hämtning av resurser: " + ex.Message);
            }
        }

        private void BekraftaBokning(object obj)
        {
            if (inloggadMedlem == null)
            {
                MessageBox.Show("Ingen inloggad användare. Logga in igen.");
                return;
            }
            if (ValdResurs == null)
            {
                MessageBox.Show("Välj en resurs i listan.");
                return;
            }
            if (string.IsNullOrEmpty(ValdStarttid) || string.IsNullOrEmpty(ValdSluttid))
            {
                MessageBox.Show("Välj starttid och sluttid.");
                return;
            }

            var starttid = ByggDateTime(ValdStarttid);
            var sluttid = ByggDateTime(ValdSluttid);

            if (sluttid <= starttid)
            {
                MessageBox.Show("Sluttiden måste vara efter starttiden.");
                return;
            }

            try
            {
                var nyBokning = new Bokning
                {
                    //MedlemID = InloggadMedlemSession.AktivMedlem.MedlemID, // döpp om den till inloggad session class namn!
                    MedlemID = inloggadMedlem.MedlemID,
                    ResursID = ValdResurs.ResursID,
                    Starttid = starttid,
                    Sluttid = sluttid,
                    SenastUppdaterad = DateTime.Now,
                    Anteckning = string.IsNullOrWhiteSpace(Anteckning) ? null : Anteckning
                };

                bokningController.SkapaBokning(nyBokning);
                MessageBox.Show($" Bokning av {ValdResurs.Namn} är genomförd!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kunde inte skapa bokning: " + ex.Message);
            }
        }

        private DateTime ByggDateTime(string tid)
        {
            var delar = tid.Split(':');
            return new DateTime(ValdDatum.Year, ValdDatum.Month, ValdDatum.Day,
                                int.Parse(delar[0]), int.Parse(delar[1]), 0);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void UppdateraProperty(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
