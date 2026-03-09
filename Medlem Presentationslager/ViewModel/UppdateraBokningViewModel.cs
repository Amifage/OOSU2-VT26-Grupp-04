using Affärslagret;
using Entitetslager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Medlem_Presentationslager.Command;

namespace Medlem_Presentationslager.ViewModel
{
    public class ÄndraAvbokaViewModel : INotifyPropertyChanged
    {
        private readonly BokningController _bokningController;
        private readonly ResursController _resursController;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string namn = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(namn));
        }

        private Medlem _inloggadMedlem;

        private Bokning _valdBokning;
        private DateTime? _nyttDatum;
        private string _nyStarttid;
        private string _nySluttid;
        private Resurs _valdResurs;
        private string _anteckning;

        public ObservableCollection<Bokning> Bokningar { get; set; }
        public ObservableCollection<Resurs> Resurser { get; set; }
        public ObservableCollection<string> Tider { get; set; }

        public Bokning ValdBokning
        {
            get => _valdBokning;
            set
            {
                _valdBokning = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HarValdBokning));
                CommandManager.InvalidateRequerySuggested();

                if (_valdBokning != null)
                {
                    NyttDatum = _valdBokning.Starttid.Date;
                    NyStarttid = _valdBokning.Starttid.ToString("HH:mm");
                    NySluttid = _valdBokning.Sluttid.ToString("HH:mm");
                    Anteckning = _valdBokning.Anteckning;

                    UppdateraLedigaResurser();

                    if (Resurser != null)
                    {
                        ValdResurs = Resurser.FirstOrDefault(r => r.ResursID == _valdBokning.ResursID);
                    }
                }
                else
                {
                    NyttDatum = null;
                    NyStarttid = null;
                    NySluttid = null;
                    Anteckning = null;
                    ValdResurs = null;
                }
            }
        }
        public bool HarValdBokning => ValdBokning != null;

        public DateTime? NyttDatum
        {
            get => _nyttDatum;
            set
            {
                _nyttDatum = value;
                OnPropertyChanged();
                UppdateraLedigaResurser();
            }
        }
        public string NyStarttid
        {
            get => _nyStarttid;
            set
            {
                _nyStarttid = value;
                OnPropertyChanged();
                UppdateraLedigaResurser();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string NySluttid
        {
            get => _nySluttid;
            set
            {
                _nySluttid = value;
                OnPropertyChanged();
                UppdateraLedigaResurser();
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public Resurs ValdResurs
        {
            get => _valdResurs;
            set
            {
                _valdResurs = value;
                OnPropertyChanged();
            }
        }

        public string Anteckning
        {
            get => _anteckning;
            set
            {
                _anteckning = value;
                OnPropertyChanged();
            }
        }

        public ICommand SparaCommand { get; }
        public ICommand AvbokaCommand { get; }
        public ICommand TillbakaCommand { get; }

        public ÄndraAvbokaViewModel(Medlem medlem)
        {
            _bokningController = new BokningController();
            _resursController = new ResursController();

            _inloggadMedlem = medlem;

            Bokningar = new ObservableCollection<Bokning>(
                 _bokningController.HämtaBokningarFörMedlem(medlem.MedlemID)
                .Where(b => b.Sluttid >= DateTime.Now)
                .OrderBy(b => b.Starttid)
            );

            Resurser = new ObservableCollection<Resurs>();
            Tider = new ObservableCollection<string>(SkapaTider());

            SparaCommand = new RelayCommand(SparaÄndringar, KanSpara);
            AvbokaCommand = new RelayCommand(AvbokaBokning, KanAvboka);
            TillbakaCommand = new RelayCommand(Tillbaka);
        }

        private void UppdateraLedigaResurser()
        {
            if (NyttDatum == null || string.IsNullOrWhiteSpace(NyStarttid) || string.IsNullOrWhiteSpace(NySluttid))
                return;

            try
            {
                DateTime start = DateTime.Parse($"{NyttDatum:yyyy-MM-dd} {NyStarttid}");
                DateTime slut = DateTime.Parse($"{NyttDatum:yyyy-MM-dd} {NySluttid}");

                if (slut <= start)
                {
                    Resurser.Clear();
                    return;
                }

                var allaResurser = _resursController.HämtaAllaResurser();
                var upptagnaBokningar = _bokningController.HämtaUpptagnaBokningar(start, slut);


                var upptagnaResursId = upptagnaBokningar
                    .Select(b => b.ResursID)
                    .Distinct()
                    .ToHashSet();

                var ledigaResurser = allaResurser
                    .Where(r => !upptagnaResursId.Contains(r.ResursID))
                    .ToList();

                Resurser.Clear();
                foreach (var resurs in ledigaResurser)
                {
                    Resurser.Add(resurs);
                }

                if (ValdResurs != null && !Resurser.Any(r => r.ResursID == ValdResurs.ResursID))
                {
                    ValdResurs = null;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel vid filtrering av resurser: " + ex.Message);
            }
        }

        private bool KanSpara(object obj)
        {
            return ValdBokning != null
                   && NyttDatum != null
                   && !string.IsNullOrWhiteSpace(NyStarttid)
                   && !string.IsNullOrWhiteSpace(NySluttid);
        }

        private void SparaÄndringar(object obj)
        {
            try
            {
                if (ValdBokning == null || NyttDatum == null)
                    return;

                DateTime start = DateTime.Parse($"{NyttDatum:yyyy-MM-dd} {NyStarttid}");
                DateTime slut = DateTime.Parse($"{NyttDatum:yyyy-MM-dd} {NySluttid}");

                if (slut <= start)
                {
                    MessageBox.Show("Sluttiden måste vara senare än starttiden.");
                    return;
                }

                ValdBokning.Starttid = start;
                ValdBokning.Sluttid = slut;
                ValdBokning.Anteckning = Anteckning;
                ValdBokning.SenastUppdaterad = DateTime.Now;

                if (ValdResurs != null)
                    ValdBokning.ResursID = ValdResurs.ResursID;

                _bokningController.UppdateraBokning(ValdBokning);

                MessageBox.Show("Bokningen uppdaterad.");
                UppdateraLista(ValdBokning.MedlemID);
                StängFönster(obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kunde inte spara ändringar: " + ex.Message);
            }
        }

        private bool KanAvboka(object obj)
        {
            return ValdBokning != null;
        }

        private void AvbokaBokning(object obj)
        {
            if (ValdBokning == null)
                return;

            var resultat = MessageBox.Show(
                "Vill du verkligen avboka denna bokning?",
                "Bekräfta",
                MessageBoxButton.YesNo);

            if (resultat == MessageBoxResult.Yes)
            {
                int medlemId = ValdBokning.MedlemID;

                _bokningController.TaBortBokning(ValdBokning.BokningsID);
                MessageBox.Show("Bokningen avbokad.");

                ValdBokning = null;
                UppdateraLista(medlemId);
                Resurser.Clear();
            }

            StängFönster(obj);
        }

        private void UppdateraLista(int medlemId)
        {
            Bokningar.Clear();

            foreach (var bokning in _bokningController.HämtaBokningarFörMedlem(medlemId)
                         .Where(b => b.Sluttid >= DateTime.Now)
                         .OrderBy(b => b.Starttid))
            {
                Bokningar.Add(bokning);
            }
        }

        private List<string> SkapaTider()
        {
            var tider = new List<string>();

            for (int timme = 7; timme <= 20; timme++)
            {
                tider.Add($"{timme:D2}:00");
                tider.Add($"{timme:D2}:30");
            }

            return tider;
        }



        private void Tillbaka(object obj)
        {

            MenyMedlem meny = new MenyMedlem(_inloggadMedlem);
            meny.Show();


            StängFönster(obj);
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