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
            if (User.CheckPassword(Password) != string.Empty)
            {
                MessageBox.Show("Invalid Credentials");
                return;
            }

            User user = ManageUser.FindUser(Username);

            if (user != null)
            {
                bool User_2fa_on = user.HaveAuth(user);

                if (User_2fa_on)                                   
                    {
                    
                    user.GetSecurityQA(out string answer, out string question);
                    InputDialog InputDialog = new InputDialog(answer, question,);
                    InputDialog.ShowDialog();

                    
                    if (twofaCorrect = true)
                    {
                        Console.WriteLine($"{user.Username}: is now logged in");
                        WorkoutWindow workoutWindow = new WorkoutWindow(user);
                        workoutWindow.Show();
                        Application.Current.Windows[0].Close();
                        return;
                    }
                        
                        
                        
                    }

                bool signedIn = user.SignIn(Username, Password);
                if(signedIn) 
                { 
                    Console.WriteLine($"{user.Username}: is now logged in");
                    WorkoutWindow workoutWindow = new WorkoutWindow(user);
                    workoutWindow.Show();
                    Application.Current.Windows[0].Close();
                }
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
