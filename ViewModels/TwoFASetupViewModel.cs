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
        private string _username;
        private string _selectedQuestion;
        private string _answer;
        public ObservableCollection<string> SecurityQuestions { get; }

        public string SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged(nameof(SelectedQuestion));
            }
        }

        public string Answer
        {
            get => _answer;
            set
            {
                _answer = value;
                OnPropertyChanged(nameof(Answer));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CreateWithout2FACommand { get; }

        public TwoFASetupViewModel(string username)
        {
            _username = username;

            //securityQuestion picks
            SecurityQuestions = new ObservableCollection<string>
            {
                "What was your childhood nickname?",
                "What is the name of your first pet?",
                "What was the model of your first car?",
                "In what city were you born?",
                "What is your favorite food?"
            };

            SubmitCommand = new RelayCommand(Submit2FASetup);
            CreateWithout2FACommand = new RelayCommand(CreateWithout2FA);
        }

        private void Submit2FASetup(object parameter)
        {
            if (string.IsNullOrEmpty(SelectedQuestion) || string.IsNullOrEmpty(Answer))
            {
                MessageBox.Show("Please select a question and provide an answer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Person user = Person.FindUser(_username);
                if (user != null)
                {
                    user.AddSecAuth(SelectedQuestion, Answer);
                    MessageBox.Show($"2FA Security Question:\nQuestion: {SelectedQuestion}\nAnswer: {Answer}", "2FA Setup", MessageBoxButton.OK, MessageBoxImage.Information);

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Application.Current.MainWindow.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong while setting up 2FA.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void CreateWithout2FA(object parameter)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
