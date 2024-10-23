using FitTrack.Commands;
using FitTrack.Users;
using FitTrack.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private string _confirmPassword;

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); }
        }

        public ICommand RegisterCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(RegisterUser);
            NavigateToLoginCommand = new RelayCommand(NavigateToLogin);
        }

        private void RegisterUser(object parameter)
        {
            try
            {
                // Kontrollera om lösenorden matchar
                if (Password == ConfirmPassword)
                {
                    // Skapa ny användare och registrera
                    Person person = new User(Username, Password, null, null, null);
                    person.RegisterUser(Username, Password);

                    // Fråga användaren om de vill lägga till 2FA (tvåfaktorsautentisering)
                    MessageBoxResult result = MessageBox.Show("Do you wish to add 2FA?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    // Om användaren väljer "Ja", navigera till 2FA (lägg till logik här)
                    if (result == MessageBoxResult.Yes)
                    {
                        // Logik för att navigera till 2FA kan läggas här
                    }
                    else
                    {
                        // Om användaren väljer "Nej", öppna huvudfönstret
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();

                        // Stäng det nuvarande registreringsfönstret
                        Application.Current.MainWindow.Close();
                    }
                }
                else
                {
                    // Om lösenorden inte matchar, visa ett felmeddelande
                    MessageBox.Show("Password does not match");
                }
            }
            catch (Exception ex)
            {
                // Fånga eventuella fel och visa ett felmeddelande
                MessageBox.Show("A problem occurred when creating a user: " + ex.Message);
            }
        }

        private void NavigateToLogin(object parameter)
        {
            // Navigera till inloggningssidan
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
