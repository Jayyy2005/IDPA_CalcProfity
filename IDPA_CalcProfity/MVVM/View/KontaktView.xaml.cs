using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json; 

namespace IDPA_CalcProfity.MVVM.View
{
    public partial class KontaktView : Page
    {
        private static readonly HttpClient client = new HttpClient();

        public KontaktView()
        {
            InitializeComponent();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string email = EmailTextBox.Text;
            string message = MessageTextBox.Text;
            string betreff = BetreffTextBox.Text;
            string betriebssystem = ((ComboBoxItem)BetriebssystemComboBox.SelectedItem)?.Content.ToString();

            // Validierungscheck
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(betriebssystem) || string.IsNullOrWhiteSpace(betreff))
            {
                StatusTextBlock.Text = "Bitte fülle alle Felder aus.";
                return;
            }

            var contactData = new
            {
                Name = name,
                Email = email,
                Message = message,
                Betreff = betreff,
                Betriebssystem = betriebssystem
            };

            var json = JsonConvert.SerializeObject(contactData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Beispiel-URL deiner API
                var response = await client.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    StatusTextBlock.Text = "Nachricht erfolgreich gesendet!";
                    NameTextBox.Clear();
                    EmailTextBox.Clear();
                    MessageTextBox.Clear();
                    BetreffTextBox.Clear();
                    BetriebssystemComboBox.SelectedIndex = -1;
                }
                else
                {
                    StatusTextBlock.Text = "Fehler beim Senden der Nachricht.";
                }
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Fehler: {ex.Message}";
            }
        }
    }
}
