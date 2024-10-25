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

        private void Login(object parameter) // login funktion
        {
            //kollar om lösenordet användaren har matat in passar modelen
            if (User.CheckPassword(Password) != string.Empty) 
            {
                //skriver ut Fel meddelande sen avbryter
                MessageBox.Show("Invalid Credentials");
                return;
            }
            
            //försöker Hittar användaren och sätter det i user
            User user = ManageUser.FindUser(Username);

            //om användaren hittades
            if (user != null)
            {
                //kollar så att kontot har 2fa 
                bool is2FAEnabled = user.HaveAuth(user);

                
                if (is2FAEnabled)
                {
                    //hämtar användarens frågor och svar för att sedan skicka vidare det till input dialog
                    user.GetSecurityQA(out string expectedAnswer, out string question);
                    //skapar ett nytt window för secuirty answer
                    InputDialog inputDialog = new InputDialog(expectedAnswer, question);
                    inputDialog.ShowDialog();

                    
                    if (inputDialog.DialogResult == true)
                    {
                        //om lösenordet är korrect logga in
                        Console.WriteLine($"{user.Username}: is now logged in");
                        WorkoutWindow workoutWindow = new WorkoutWindow(user);
                        workoutWindow.Show();
                        Application.Current.Windows[0].Close();
                        return;
                    }
                    else
                    {
                        //inte korrekt
                        MessageBox.Show("The answer to the security question was incorrect.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

                //om användaren inte har 2fa logga bara in
                bool signedIn = user.SignIn(Username, Password);
                if (signedIn)
                {
                    Console.WriteLine($"{user.Username}: is now logged in");
                    WorkoutWindow workoutWindow = new WorkoutWindow(user);
                    workoutWindow.Show();
                    Application.Current.Windows[0].Close();
                }
            }
        }

        //navigerar till registerwindow
        private void SignUp(object parameter)
        {
            
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Application.Current.Windows[0].Close();
        }

        //navigerar till forgotpassword
        private void ForgotPassword(object parameter)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
