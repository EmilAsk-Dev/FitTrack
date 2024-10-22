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
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
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
                if (Password == ConfirmPassword)
                {
                    Person person = new User(Username, Password, null, null, null);
                    person.RegisterUser(Username, Password);

                    MessageBoxResult result = MessageBox.Show("Do you wish to add 2FA", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Navigate to 2FA
                        
                    }
                    else
                    {
                        
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        Application.Current.MainWindow.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Passwords do not match. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while registering the user. Please try again.");
            }
        }

        private void NavigateToLogin(object parameter)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
