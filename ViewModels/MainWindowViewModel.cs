using System;
using System.Security.Cryptography.Xml;
using System.Windows;
using System.Windows.Input;
using FitTrack.Commands;
using FitTrack.Users;
using FitTrack.Windows;

namespace FitTrack.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private bool signedIn; // Variabel för att kontrollera om användaren är inloggad
        private string username; // Variabel för användarnamn
        private string password; // Variabel för lösenord

        // Egenskap för användarnamn
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        // Egenskap för lösenord
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        static bool firstTimeOpen = true; // Variabel för att kontrollera första öppningen av applikationen

        // Kommandon för att hantera olika åtgärder
        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }
        public ICommand ForgotPasswordCommand { get; }

        // Konstruktor för att initiera kommandon och eventuellt lägga till användare och träningspass
        public MainWindowViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            SignUpCommand = new RelayCommand(SignUp);
            ForgotPasswordCommand = new RelayCommand(ForgotPassword);
            if (firstTimeOpen)
            {
                AddUsersAndWorkouts.AddUsersAndWorkoutsToDatabase(); // Lägger till användare och träningspass i databasen
                firstTimeOpen = false; // Sätter flaggan till false efter första öppningen
            }
        }

        // Metod för att hantera inloggning
        private void Login(object parameter)
        {
            User user = ManageUser.FindUser(Username); // Försök att hitta användaren

            // Kontrollera om användaren finns
            if (user == null)
            {
                MessageBox.Show("Användare hittades inte", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Kontrollera om inloggningsuppgifterna är korrekta
            bool correctCredentials = user.SignIn(username, password);
            if (!correctCredentials)
            {
                MessageBox.Show("Ogiltiga inloggningsuppgifter", "Fel", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool is2FAEnabled = user.HaveAuth(user); // Kontrollera om tvåfaktorsautentisering är aktiverad

            if (is2FAEnabled)
            {
                // Hämta säkerhetsfrågan
                user.GetSecurityQA(out string expectedAnswer, out string question);

                InputDialog inputDialog = new InputDialog(expectedAnswer, question); // Skapa en ny dialogruta för inmatning

                bool? result = inputDialog.ShowDialog(); // Visa dialogrutan

                // Kontrollera resultatet av dialogrutan
                if (result == true)
                {
                    Console.WriteLine($"{user.Username}: är nu inloggad"); // Logga inloggning
                    ManageUser.currentUser = user; // Spara aktuell användare
                    WorkoutWindow workoutWindow = new WorkoutWindow(user); // Öppna träningsfönster
                    workoutWindow.Show();
                    Application.Current.Windows[0].Close(); // Stäng nuvarande fönster
                }
                else
                {
                    // Meddelande om felaktigt svar på säkerhetsfrågan
                    MessageBox.Show("Svaret på säkerhetsfrågan var felaktigt.", "Åtkomst nekad", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                // Logga in om tvåfaktorsautentisering inte är aktiverad
                Console.WriteLine($"{user.Username}: är nu inloggad");
                WorkoutWindow workoutWindow = new WorkoutWindow(user);
                workoutWindow.Show();
                Application.Current.Windows[0].Close(); // Stäng nuvarande fönster
            }
        }

        // Metod för att hantera registrering
        private void SignUp(object parameter)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show(); // Visa registreringsfönstret
            Application.Current.Windows[0].Close(); // Stäng nuvarande fönster
        }

        // Metod för att hantera glömt lösenord
        private void ForgotPassword(object parameter)
        {
            ForgotPassword forgotPasswordWindow = new ForgotPassword();
            forgotPasswordWindow.Show(); // Visa fönster för glömt lösenord
            Application.Current.Windows[0].Close(); // Stäng nuvarande fönster
        }
    }
}
