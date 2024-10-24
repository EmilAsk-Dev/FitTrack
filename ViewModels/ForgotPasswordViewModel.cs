using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using FitTrack.Commands;
using FitTrack.Users;
using FitTrack.Views;

namespace FitTrack.ViewModels
{
    public class ForgotPasswordViewModel : INotifyPropertyChanged
    {
        private int _step = 1;
        private User _user;
        private string _username;
        private string _securityAnswer;
        private string _newPassword;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string SecurityAnswer
        {
            get => _securityAnswer;
            set
            {
                _securityAnswer = value;
                OnPropertyChanged(nameof(SecurityAnswer));
            }
        }

        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        public ICommand ResetPasswordCommand { get; }
        public ICommand LoginCommand { get; }

        public ForgotPasswordViewModel()
        {
            ResetPasswordCommand = new RelayCommand(param => ResetPassword());
            LoginCommand = new RelayCommand(param => OpenLoginWindow());
        }

        private void ResetPassword()
        {
            switch (_step)
            {
                case 1:
                    if (string.IsNullOrEmpty(Username))
                    {
                        MessageBox.Show("Please enter your username.");
                        return;
                    }

                    _user = ManageUser.FindUser(Username) as User;

                    if (_user != null)
                    {
                        
                        Step = 2;
                    }
                    else
                    {
                        MessageBox.Show("Username doesn't exist.");
                    }
                    break;

                case 2:
                    if (string.IsNullOrEmpty(SecurityAnswer))
                    {
                        MessageBox.Show("Please enter your security answer.");
                        return;
                    }

                    bool securityCheck = _user.IfSecurityAnswer(SecurityAnswer);
                    if (securityCheck)
                    {
                        
                        Step = 3;
                    }
                    else
                    {
                        MessageBox.Show("Wrong security answer.");
                    }
                    break;

                case 3:
                    if (string.IsNullOrEmpty(NewPassword))
                    {
                        MessageBox.Show("Please enter a new password.");
                        return;
                    }

                    User.ResetPassword(_user.Username, NewPassword);

                    
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Application.Current.Shutdown();
                    break;

                default:
                    break;
            }
        }

        private void OpenLoginWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int step;
        public int Step
        {
            get => step;
            set
            {
                step = value;
                OnPropertyChanged(nameof(Step));
            }
        }
    }
}
