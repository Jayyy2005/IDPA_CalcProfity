using System;
using System.ComponentModel;

namespace IDPA_CalcProfity.MVVM.ViewModel
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        // Jahresgewinn speichern
        private string _jahresgewinn = string.Empty;
        public string Jahresgewinn
        {
            get => _jahresgewinn;
            set
            {
                _jahresgewinn = value;
                OnPropertyChanged(nameof(Jahresgewinn));
            }
        }

        // Aktienkapital speichern
        private string _aktienkapital = string.Empty;
        public string Aktienkapital
        {
            get => _aktienkapital;
            set
            {
                _aktienkapital = value;
                OnPropertyChanged(nameof(Aktienkapital));
            }
        }

        // Gesetzliche Reserve speichern
        private string _gesetzlicheReserve = string.Empty;
        public string GesetzlicheReserve
        {
            get => _gesetzlicheReserve;
            set
            {
                _gesetzlicheReserve = value;
                OnPropertyChanged(nameof(GesetzlicheReserve));
            }
        }

        // Bilanzvortrag speichern
        private string _bilanzvortrag = string.Empty;
        public string Bilanzvortrag
        {
            get => _bilanzvortrag;
            set
            {
                _bilanzvortrag = value;
                OnPropertyChanged(nameof(Bilanzvortrag));
            }
        }

        // Gewünschte Dividende speichern
        private string _gewuenschteDividende = string.Empty;
        public string GewuenschteDividende
        {
            get => _gewuenschteDividende;
            set
            {
                _gewuenschteDividende = value;
                OnPropertyChanged(nameof(GewuenschteDividende));
            }
        }

        // Erster Beitrag zur Reserve speichern
        private string _ersterBeitragReserveOutput = string.Empty;
        public string ErsterBeitragReserveOutput
        {
            get => _ersterBeitragReserveOutput;
            set
            {
                _ersterBeitragReserveOutput = value;
                OnPropertyChanged(nameof(ErsterBeitragReserveOutput));
            }
        }

        // Gesamte Reserven speichern
        private string _gesamtReservenOutput = string.Empty;
        public string GesamtReservenOutput
        {
            get => _gesamtReservenOutput;
            set
            {
                _gesamtReservenOutput = value;
                OnPropertyChanged(nameof(GesamtReservenOutput));
            }
        }

        // Grunddividende speichern
        private string _grundDividendeOutput = string.Empty;
        public string GrundDividendeOutput
        {
            get => _grundDividendeOutput;
            set
            {
                _grundDividendeOutput = value;
                OnPropertyChanged(nameof(GrundDividendeOutput));
            }
        }

        // Superdividende speichern
        private string _superDividendeOutput = string.Empty;
        public string SuperDividendeOutput
        {
            get => _superDividendeOutput;
            set
            {
                _superDividendeOutput = value;
                OnPropertyChanged(nameof(SuperDividendeOutput));
            }
        }

        // Neuer Gewinn-/Verlustvortrag speichern
        private string _neuerGewinnVortragOutput = string.Empty;
        public string NeuerGewinnVortragOutput
        {
            get => _neuerGewinnVortragOutput;
            set
            {
                _neuerGewinnVortragOutput = value;
                OnPropertyChanged(nameof(NeuerGewinnVortragOutput));
            }
        }

        // Event für PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged; 
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Ergebnisse berechnen
        public void CalculateResults()
        {
            ErsterBeitragReserveOutput = "Calculated Value";
            GesamtReservenOutput = "Calculated Value";
            GrundDividendeOutput = "Calculated Value";
            SuperDividendeOutput = "Calculated Value";
            NeuerGewinnVortragOutput = "Calculated Value";
        }

        // Daten zurücksetzen
        public void ClearData()
        {
            Jahresgewinn = "";
            Aktienkapital = "";
            GesetzlicheReserve = "";
            Bilanzvortrag = "";
            GewuenschteDividende = "";
            ErsterBeitragReserveOutput = "";
            GesamtReservenOutput = "";
            GrundDividendeOutput = "";
            SuperDividendeOutput = "";
            NeuerGewinnVortragOutput = "";
        }
    }
}
