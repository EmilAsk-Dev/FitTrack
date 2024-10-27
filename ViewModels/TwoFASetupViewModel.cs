using FitTrack.Commands;
using FitTrack.Users;
using FitTrack.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModels
{
    public class TwoFASetupViewModel : BaseViewModel
    {
        private string _username; // Variabel för användarnamn
        private string _selectedQuestion; // Variabel för valt säkerhetsfråga
        private string _answer; // Variabel för svar på säkerhetsfråga
        public ObservableCollection<string> SecurityQuestions { get; } // Lista över säkerhetsfrågor

        // Egenskap för vald säkerhetsfråga
        public string SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged(nameof(SelectedQuestion)); // Meddela om ändring
            }
        }

        // Egenskap för svar på säkerhetsfråga
        public string Answer
        {
            get => _answer;
            set
            {
                _answer = value;
                OnPropertyChanged(nameof(Answer)); // Meddela om ändring
            }
        }

        // Kommandon för att hantera inlämning och skapande utan 2FA
        public ICommand SubmitCommand { get; }
        public ICommand CreateWithout2FACommand { get; }

        // Konstruktor för att initiera kommandon och säkerhetsfrågor
        public TwoFASetupViewModel(string username)
        {
            _username = username;

            // Lista över säkerhetsfrågor
            SecurityQuestions = new ObservableCollection<string>
            {
                "Vad var ditt barndomsnamn?",
                "Vad hette ditt första husdjur?",
                "Vilken modell var din första bil?",
                "I vilken stad föddes du?",
                "Vad är din favoritmat?"
            };

            SubmitCommand = new RelayCommand(Submit2FASetup); // Initiera inlämningskommando
            CreateWithout2FACommand = new RelayCommand(CreateWithout2FA); // Initiera kommando för skapande utan 2FA
        }

        // Metod för att hantera inlämning av 2FA-inställningar
        private void Submit2FASetup(object parameter)
        {
            // Kontrollera att både fråga och svar är ifyllda
            if (string.IsNullOrEmpty(SelectedQuestion) || string.IsNullOrEmpty(Answer))
            {
                MessageBox.Show("Vänligen välj en fråga och ge ett svar.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                User user = ManageUser.FindUser(_username); // Hitta användaren
                if (user != null)
                {
                    user.AddSecAuth(SelectedQuestion, Answer); // Lägg till säkerhetsautentisering
                    MessageBox.Show($"2FA Säkerhetsfråga:\nFråga: {SelectedQuestion}\nSvar: {Answer}", "2FA Inställning", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow mainWindow = new MainWindow(); // Öppna huvudfönster
                    mainWindow.Show();
                    Application.Current.Windows[0].Close(); // Stäng nuvarande fönster
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Något gick fel vid inställning av 2FA.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine($"Fel: {ex.Message}"); // Logga felmeddelande
            }
        }

        // Metod för att skapa konto utan 2FA
        private void CreateWithout2FA(object parameter)
        {
            MainWindow mainWindow = new MainWindow(); // Öppna huvudfönster
            mainWindow.Show();
            Application.Current.Windows[0].Close(); // Stäng nuvarande fönster
        }
    }
}
