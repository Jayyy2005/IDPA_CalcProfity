using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IDPA_CalcProfity.MVVM.View
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        // Klick-Event für "Berechnen"-Button
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // Überprüfen auf leere Felder
            if (string.IsNullOrWhiteSpace(Jahresgewinn.Text) ||
                string.IsNullOrWhiteSpace(Aktienkapital.Text) ||
                string.IsNullOrWhiteSpace(gesetzlicheReserve.Text) ||
                string.IsNullOrWhiteSpace(Bilanzvortrag.Text) ||
                string.IsNullOrWhiteSpace(gewuenschteDividende.Text))
            {
                MessageBox.Show("Bitte füllen Sie alle erforderlichen Felder aus.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Beenden bei leeren Feldern
            }

            try
            {
                // Eingabewerte parsen
                float jahresgewinn = float.Parse(Jahresgewinn.Text);
                float aktienPz = float.Parse(Aktienkapital.Text);
                float gesGewinnres = float.Parse(gesetzlicheReserve.Text);
                float gewinnVerlustvortrag = float.Parse(Bilanzvortrag.Text);
                float gewünschteDividende = float.Parse(gewuenschteDividende.Text);

                // Überprüfen auf gültiges Aktienkapital
                if (aktienPz <= 0)
                {
                    MessageBox.Show("Das Aktienkapital muss grösser als 0 sein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; // Beenden bei ungültigem Kapital
                }

                // Überprüfen, ob die Dividende den Gewinn übersteigt
                if (gewünschteDividende > jahresgewinn && jahresgewinn > 0)
                {
                    MessageBox.Show("Die gewünschte Dividende darf den Jahresgewinn nicht übersteigen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; // Beenden bei ungültiger Dividende
                }

                // Berechnung der Reserven
                float aktien05 = aktienPz * 0.5f;
                float ersterBeitragZurReserve = 0;

                if (jahresgewinn > 0 && gesGewinnres < aktien05)
                {
                    float maxBeitragZurReserve = aktien05 - gesGewinnres;
                    ersterBeitragZurReserve = 0.05f * jahresgewinn;

                    // Begrenzung des Beitrags
                    if (ersterBeitragZurReserve > maxBeitragZurReserve)
                    {
                        ersterBeitragZurReserve = maxBeitragZurReserve;
                    }
                }

                // Berechnung von Dividenden
                float grundDividende = aktienPz * 0.05f;
                float superDividende = (gewünschteDividende > grundDividende) ? (gewünschteDividende - grundDividende) : 0;

                // Neuer Gewinn-/Verlustvortrag
                float neuerGewinnVerlustvortrag = gewinnVerlustvortrag + (jahresgewinn - gewünschteDividende - ersterBeitragZurReserve);

                // Ausgabe der Ergebnisse mit Farben und Tooltips
                ErsterBeitragReserveOutput.Text = $"{ersterBeitragZurReserve:0.00} CHF";
                ErsterBeitragReserveOutput.Foreground = ersterBeitragZurReserve >= 0 ? Brushes.Green : Brushes.Red;
                ErsterBeitragReserveOutput.ToolTip = ersterBeitragZurReserve >= 0 ? "Erster Beitrag zur Reserve." : "Negativer Beitrag zur Reserve.";

                GesamtReservenOutput.Text = $"{(gesGewinnres + ersterBeitragZurReserve):0.00} CHF";
                GesamtReservenOutput.Foreground = (gesGewinnres + ersterBeitragZurReserve) >= 0 ? Brushes.Green : Brushes.Red;
                GesamtReservenOutput.ToolTip = (gesGewinnres + ersterBeitragZurReserve) >= 0 ? "Gesamte Reserven." : "Negative Reserven.";

                GrundDividendeOutput.Text = $"{grundDividende:0.00} CHF";
                GrundDividendeOutput.Foreground = grundDividende >= 0 ? Brushes.Green : Brushes.Red;
                GrundDividendeOutput.ToolTip = "Grunddividende beträgt 5 % des Aktienkapitals.";

                SuperDividendeOutput.Text = $"{superDividende:0.00} CHF";
                SuperDividendeOutput.Foreground = superDividende >= 0 ? Brushes.Green : Brushes.Red;
                SuperDividendeOutput.ToolTip = "Superdividende über Grunddividende.";

                NeuerGewinnVortragOutput.Text = $"{neuerGewinnVerlustvortrag:0.00} CHF";
                NeuerGewinnVortragOutput.Foreground = neuerGewinnVerlustvortrag >= 0 ? Brushes.Green : Brushes.Red;
                NeuerGewinnVortragOutput.ToolTip = neuerGewinnVerlustvortrag >= 0 ? "Gewinnvortrag." : "Verlustvortrag.";

                // Überprüfen auf Verlust bei Dividende
                if (neuerGewinnVerlustvortrag < 0 && gewünschteDividende > 0)
                {
                    MessageBox.Show("Keine Dividende bei Verlust.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; // Beenden bei Verlust
                }
            }
            catch (FormatException)
            {
                // Behandeln ungültiger Eingaben
                MessageBox.Show("Bitte gültige numerische Werte eingeben.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Behandeln unerwarteter Fehler
                MessageBox.Show("Unerwarteter Fehler: " + ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Klick-Event für "Zurücksetzen"-Button
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            // Leeren aller Felder
            Jahresgewinn.Clear();
            Aktienkapital.Clear();
            gesetzlicheReserve.Clear();
            Bilanzvortrag.Clear();
            gewuenschteDividende.Clear();
            ErsterBeitragReserveOutput.Text = string.Empty;
            GesamtReservenOutput.Text = string.Empty;
            GrundDividendeOutput.Text = string.Empty;
            SuperDividendeOutput.Text = string.Empty;
            NeuerGewinnVortragOutput.Text = string.Empty;
        }

        // Klick-Event für "Speichern"-Button
        private void SpeichernButton_Click(object sender, RoutedEventArgs e)
        {
            // Überprüfen auf Berechnung
            if (string.IsNullOrWhiteSpace(ErsterBeitragReserveOutput.Text) ||
                string.IsNullOrWhiteSpace(GesamtReservenOutput.Text) ||
                string.IsNullOrWhiteSpace(GrundDividendeOutput.Text) ||
                string.IsNullOrWhiteSpace(SuperDividendeOutput.Text) ||
                string.IsNullOrWhiteSpace(NeuerGewinnVortragOutput.Text))
            {
                MessageBox.Show("Bitte zuerst berechnen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Beenden bei fehlender Berechnung
            }

            // Text für die Zwischenablage vorbereiten
            string kopierText = $"Erster Beitrag zur Reserve: {ErsterBeitragReserveOutput.Text}\n" +
                                $"Gesamte Reserven: {GesamtReservenOutput.Text}\n" +
                                $"Grunddividende: {GrundDividendeOutput.Text}\n" +
                                $"Superdividende: {SuperDividendeOutput.Text}\n" +
                                $"Neuer Gewinn-/Verlustvortrag: {NeuerGewinnVortragOutput.Text}";

            Clipboard.SetText(kopierText); // Text in die Zwischenablage kopieren

            MessageBox.Show("Berechnungen in die Zwischenablage kopiert.", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Event beim Ändern des Textes in Eingabefeldern
        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Eingabefelder auf gültige Zahlen überprüfen
            void CheckTextBox(TextBox textBox)
            {
                if (!string.IsNullOrWhiteSpace(textBox.Text) && !IsValidDecimal(textBox.Text))
                {
                    textBox.Background = Brushes.Red; // Ungültige Eingabe rot markieren
                    textBox.ToolTip = "Nur Zahlen und Komma für Dezimalzahlen verwenden.";
                }
                else
                {
                    textBox.Background = Brushes.White; // Gültige Eingabe zurücksetzen
                    textBox.ToolTip = null;
                }
            }

            // Validierung der Dezimalzahlen
            bool IsValidDecimal(string input)
            {
                return Regex.IsMatch(input, @"^-?\d+([.,]\d+)?$"); // Format prüfen
            }

            // Überprüfung aller Eingabefelder
            CheckTextBox(Jahresgewinn);
            CheckTextBox(Aktienkapital);
            CheckTextBox(gesetzlicheReserve);
            CheckTextBox(Bilanzvortrag);
            CheckTextBox(gewuenschteDividende);

            // Überprüfen, ob die Dividende den Gewinn übersteigt
            if (float.TryParse(Jahresgewinn.Text, out float jahresgewinn) &&
                float.TryParse(gewuenschteDividende.Text, out float gewünschteDividende))
            {
                if (gewünschteDividende > jahresgewinn)
                {
                    Jahresgewinn.Background = Brushes.Red; // Hintergrund rot
                    gewuenschteDividende.Background = Brushes.Red;
                    Jahresgewinn.ToolTip = "Jahresgewinn muss größer sein."; // ToolTip setzen
                    gewuenschteDividende.ToolTip = "Dividende muss kleiner sein.";
                }
                else
                {
                    Jahresgewinn.Background = Brushes.White; // Hintergrund zurücksetzen
                    gewuenschteDividende.Background = Brushes.White;
                    Jahresgewinn.ToolTip = null;
                    gewuenschteDividende.ToolTip = null;
                }
            }
        }
    }
}
