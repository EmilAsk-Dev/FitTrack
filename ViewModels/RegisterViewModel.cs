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
        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _selectedCountry;

        private readonly RegisterWindow _registerWindow;

        public ObservableCollection<string> CountryList { get; } = new ObservableCollection<string>
        {
            "United States", "Canada", "United Kingdom", "Australia", "Germany", "France", "Sweden"
        };

        public string SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                _selectedCountry = value;
                OnPropertyChanged(nameof(SelectedCountry));
            }
        }

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }

        public string Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(nameof(Password)); }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { _confirmPassword = value; OnPropertyChanged(nameof(ConfirmPassword)); }
        }

        public ICommand RegisterCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

        public RegisterViewModel(RegisterWindow registerWindow)
        {
            _registerWindow = registerWindow;
            RegisterCommand = new RelayCommand(RegisterUser);
            NavigateToLoginCommand = new RelayCommand(NavigateToLogin);
        }

        private void RegisterUser(object parameter)
        {
            try
            {
                if (Password != ConfirmPassword)
                {
                    MessageBox.Show("Passwords do not match.");
                    return;
                }



                User user = new User(Username, Password, SelectedCountry, null, null);

                bool userAlreadyExist = User.IfUserExist(Username);
                if (!userAlreadyExist)
                {
                    user.RegisterUser(Username, Password);

                    MessageBoxResult result = MessageBox.Show("Do you wish to add 2FA?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        _registerWindow.MainFrame.Navigate(new TwoFASetupPage(Username));
                    }
                    else
                    {
                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                        Application.Current.Windows[0].Close();
                    }
                }
                else
                {
                    MessageBox.Show("User does already exist");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A problem occurred: " + ex.Message);
            }
        }

        private void NavigateToLogin(object parameter)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
