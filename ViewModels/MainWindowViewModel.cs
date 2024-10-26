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
        private bool signedIn;
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

        private void Login(object parameter) // login function
        {
            // Check if the user exists
            User user = ManageUser.FindUser(Username);

            // If the user is not found, show an error message
            if (user == null)
            {
                MessageBox.Show("User not found", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            // Validate the password
            bool correctCredentials = User.ValidateUserAndPass(user, username, password);
            if (!correctCredentials)
            {
                MessageBox.Show("Invalid Credentials", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }                
                        

            // Proceed to check if 2FA is enabled
            bool is2FAEnabled = user.HaveAuth(user);

            if (is2FAEnabled)
            {
                // 2FA Process
                // Get the expected answer and question
                user.GetSecurityQA(out string expectedAnswer, out string question);

                // Create and show the InputDialog with required parameters
                InputDialog inputDialog = new InputDialog(expectedAnswer, question);

                bool? result = inputDialog.ShowDialog(); // Show dialog and wait for result

                if (result == true) // If user confirmed the 2FA answer
                {
                    // Successful login process
                    Console.WriteLine($"{user.Username}: is now logged in");
                    ManageUser.currentUser = user;
                    WorkoutWindow workoutWindow = new WorkoutWindow(user);
                    workoutWindow.Show();
                    Application.Current.Windows[0].Close();
                }
                else
                {
                    // User denied or answered incorrectly
                    MessageBox.Show("The answer to the security question was incorrect.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                // If 2FA is not enabled, directly log in
                Console.WriteLine($"{user.Username}: is now logged in");
                WorkoutWindow workoutWindow = new WorkoutWindow(user);
                workoutWindow.Show();
                Application.Current.Windows[0].Close();
            }
        }

        // Navigates to the RegisterWindow
        private void SignUp(object parameter)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Application.Current.Windows[0].Close();
        }

        // Navigates to the ForgotPassword window
        private void ForgotPassword(object parameter)
        {
            ForgotPassword forgotPasswordWindow = new ForgotPassword();
            forgotPasswordWindow.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
