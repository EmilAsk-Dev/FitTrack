using FitTrack.Commands;
using FitTrack.Users;
using FitTrack.Views;
using FitTrack.Windows;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _username; // Variabel för användarnamn
        private string _password; // Variabel för lösenord
        private string _confirmPassword; // Variabel för bekräftelse av lösenord
        private string _selectedCountry; // Variabel för valt land

        private readonly RegisterWindow _registerWindow; // Referens till registreringsfönstret

        // Lista över länder som kan väljas
        public ObservableCollection<string> CountryList { get; } = new ObservableCollection<string>
        {
            "United States", "Canada", "United Kingdom", "Australia", "Germany", "France", "Sweden"
        };

        // Egenskap för valt land
        public string SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                _selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry)); // Meddela om ändring
            }
        }

        // Egenskap för användarnamn
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username)); } // Meddela om ändring
        }

        // Egenskap för lösenord
        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); } // Meddela om ändring
        }

        // Egenskap för bekräftelse av lösenord
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); } // Meddela om ändring
        }

        // Kommandon för att hantera registrering och navigering
        public ICommand RegisterCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

        // Konstruktor för att initiera kommandon och spara referens till registreringsfönstret
        public RegisterViewModel(RegisterWindow registerWindow)
        {
            _registerWindow = registerWindow; // Spara referens till registreringsfönstret
            RegisterCommand = new RelayCommand(RegisterUser); // Initiera registreringskommando
            NavigateToLoginCommand = new RelayCommand(NavigateToLogin); // Initiera navigeringskommando
        }

        // Metod för att registrera användaren
        private void RegisterUser(object parameter)
        {
            try
            {
                // Kontrollera om lösenorden matchar
                if (Password != ConfirmPassword)
                {
                    MessageBox.Show("Lösenorden matchar inte.");
                    return;
                }

                User user = new User(Username, Password, SelectedCountry, null, null); // Skapa en ny användare

                bool userAlreadyExist = User.IfUserExist(Username); // Kontrollera om användaren redan finns
                if (!userAlreadyExist)
                {
                    user.RegisterUser(Username, Password); // Registrera användaren

                    // Fråga användaren om de vill lägga till tvåfaktorsautentisering
                    MessageBoxResult result = MessageBox.Show("Vill du lägga till 2FA?", "Bekräftelse", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        _registerWindow.MainFrame.Navigate(new TwoFASetupPage(Username)); // Navigera till 2FA-inställningssidan
                    }
                    else
                    {
                        var mainWindow = new MainWindow(); // Öppna huvudsidan
                        mainWindow.Show();
                        Application.Current.Windows[0].Close(); // Stäng nuvarande fönster
                    }
                }
                else
                {
                    MessageBox.Show("Användaren finns redan"); // Meddelande om att användaren redan finns
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ett problem inträffade: " + ex.Message); // Meddelande vid fel
            }
        }

        // Metod för att navigera till inloggningssidan
        private void NavigateToLogin(object parameter)
        {
            MainWindow loginWindow = new MainWindow(); // Skapa nytt inloggningsfönster
            loginWindow.Show();
            Application.Current.Windows[0].Close(); // Stäng nuvarande fönster
        }
    }
}
