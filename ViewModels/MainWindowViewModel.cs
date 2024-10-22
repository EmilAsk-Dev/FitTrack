using System.Windows;
using System.Windows.Input;
using FitTrack.Commands;
using FitTrack.Users;
using FitTrack.Windows;

namespace FitTrack.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string username;
        private string password;

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }
        public ICommand ForgotPasswordCommand { get; }

        public MainWindowViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            SignUpCommand = new RelayCommand(SignUp);
            ForgotPasswordCommand = new RelayCommand(ForgotPassword);
            AddUsersAndWorkouts.AddUsersAndWorkoutsToDatabase();
        }

        private void Login(object parameter)
        {
            if (Person.CheckPassword(Password) != string.Empty)
            {
                MessageBox.Show("Invalid Credentials");
                return;
            }

            Person user = Person.FindUser(Username);

            if (user != null)
            {
                bool signedIn = user.SignIn(Username, Password);
                WorkoutWindow workoutWindow = new WorkoutWindow(user);
                workoutWindow.Show();
                Application.Current.Windows[0].Close();
            }
        }

        private void SignUp(object parameter)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Application.Current.Windows[0].Close();
        }

        private void ForgotPassword(object parameter)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
