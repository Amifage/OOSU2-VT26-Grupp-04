using Affärslagret;
using Entitetslager;
using Medlem_Presentationslager.Command;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;



namespace Medlem_Presentationslager.ViewModel
{
    public class SkapaMedlemViewModel : INotifyPropertyChanged
    {
        private string _namn;
        private string _epost;
        private string _telefonnummer;
        private string _medlemsNivå;
        private string _betalstatus;
        private string _lösenord;
        private string _felmeddelande;

        private readonly MedlemController _medlemController;

        #region Properties
        public string Namn
        {
            get => _namn;
            set
            {
                _namn = value;
                OnPropertyChanged();
            }
        }

        public string Epost
        {
            get => _epost;
            set
            {
                _epost = value;
                OnPropertyChanged();
            }
        }

        public string Telefonnummer
        {
            get => _telefonnummer;
            set
            {
                _telefonnummer = value;
                OnPropertyChanged();
            }
        }

        public List<string> MedlemsNivåer { get; } = new List<string>
        {
            "Företag",
            "Fast",
            "Flex"
        };
        public string MedlemsNivå
        {
            get => _medlemsNivå;
            set
            {
                _medlemsNivå = value;
                OnPropertyChanged();
            }
        }

        public List<string> Betalstatusar { get; } = new List<string>
        {
            "Betald",
            "Obetald"
        };
        public string Betalstatus
        {
            get => _betalstatus;
            set
            {
                _betalstatus = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string namn = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(namn));
        }
        #endregion

        public ICommand TillbakaCommand { get; }
        public ICommand OpenSkapaKontoCommand { get; }


        #region Skapa medlem kod
        public SkapaMedlemViewModel() 
        {
            _medlemController = new MedlemController();
            TillbakaCommand = new RelayCommand(Tillbaka);
            OpenSkapaKontoCommand = new RelayCommand(SparaMedlem);

        }

        private void SparaMedlem(object obj)
        {
            try
            {
                string telefon = Telefonnummer?.Replace(" ", "").Trim();

                if (string.IsNullOrWhiteSpace(Namn) ||
                    string.IsNullOrWhiteSpace(Epost) ||
                    string.IsNullOrWhiteSpace(telefon) ||
                    string.IsNullOrWhiteSpace(MedlemsNivå) ||
                    string.IsNullOrWhiteSpace(Betalstatus) ||
                    string.IsNullOrWhiteSpace(Lösenord))
                {
                    MessageBox.Show("Du måste fylla i samtliga uppgifter.");
                    return;
                }

                if (!IsDigitsOnly(telefon))
                {
                    MessageBox.Show("Telefonnummer får bara innehålla siffror.");
                    return;
                }

                var nyMedlem = new Medlem
                {
                    Namn = Namn.Trim().ToLower(),
                    Epost = Epost.Trim().ToLower(),
                    Telefonnummer = telefon,
                    Medlemsnivå = MedlemsNivå.ToLower(),
                    Betalstatus = Betalstatus.ToLower(),
                    Lösenord = Lösenord.ToLower(),
                    SenastUppdaterad = DateTime.Now
                };

                _medlemController.SkapaMedlem(nyMedlem);

                MessageBox.Show($"Medlemmen har sparats!\n\nTilldelat medlemsnummer: {nyMedlem.MedlemID}");

                Tillbaka(obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel vid sparning: " + ex.Message);
            }
        
        }
        #endregion


        #region Diverse metoder
        static bool IsDigitsOnly(string str) //Denna funktion tar en sträng och returnerar falskt om villkoret inte stämmer och sant om villkoret stämmer. Funktionen kontrollerar så strängen innehåller siffror mellan 0-9.
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        private void Tillbaka(object obj)
        {
         
            MedlemLogin login = new MedlemLogin();
            login.Show();

     
            StängFönster(obj);
        }
        private void StängFönster(object parameter)
        {
            if (parameter is Window fönster)
            {
                fönster.Close();
            }
        }
        #endregion
    }
}
