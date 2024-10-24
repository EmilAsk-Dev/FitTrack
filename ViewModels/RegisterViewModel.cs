using FitTrack.Commands;
using FitTrack.Users;
using FitTrack.Views;
using FitTrack.Windows;
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

        private readonly RegisterWindow _registerWindow; // Store a reference to RegisterWindow

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

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

        // Modify constructor to accept RegisterWindow
        public RegisterViewModel(RegisterWindow registerWindow)
        {
            _registerWindow = registerWindow; // Assign it to the private field
            RegisterCommand = new RelayCommand(RegisterUser);
            NavigateToLoginCommand = new RelayCommand(NavigateToLogin);
        }

        private void RegisterUser(object parameter)
        {
            try
            {
                // Check if the passwords match
                if (Password != ConfirmPassword)
                {
                    MessageBox.Show("Passwords do not match.");
                    return;
                }

                // Create new user and register
                User user = new User(Username, Password, null, null, null);
                user.RegisterUser(Username, Password);

                // Ask the user if they want to add 2FA
                MessageBoxResult result = MessageBox.Show("Do you wish to add 2FA?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Navigate to the TwoFASetupPage in the Frame of RegisterWindow
                    _registerWindow.MainFrame.Navigate(new TwoFASetupPage(Username));
                }
                else
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    Application.Current.Windows[0].Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A problem occurred: " + ex.Message);
            }
        }

        private void NavigateToLogin(object parameter)
        {
            // Navigate to the login page
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
