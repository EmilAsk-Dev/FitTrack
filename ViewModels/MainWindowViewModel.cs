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

        static bool firstTimeOpen = true;

        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }
        public ICommand ForgotPasswordCommand { get; }

        public MainWindowViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            SignUpCommand = new RelayCommand(SignUp);
            ForgotPasswordCommand = new RelayCommand(ForgotPassword);
            if (firstTimeOpen)
            {
                AddUsersAndWorkouts.AddUsersAndWorkoutsToDatabase();
                firstTimeOpen = false;
            }
            
        }

        private void Login(object parameter) 
        {
            
            User user = ManageUser.FindUser(Username);

            
            if (user == null)
            {
                MessageBox.Show("User not found", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            bool correctCredentials = user.SignIn( username, password);
            if (!correctCredentials)
            {
                MessageBox.Show("Invalid Credentials", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }                
                        

            
            bool is2FAEnabled = user.HaveAuth(user);

            if (is2FAEnabled)
            {
                
                user.GetSecurityQA(out string expectedAnswer, out string question);

                
                InputDialog inputDialog = new InputDialog(expectedAnswer, question);

                bool? result = inputDialog.ShowDialog(); 

                if (result == true) 
                {
                    
                    Console.WriteLine($"{user.Username}: is now logged in");
                    ManageUser.currentUser = user;
                    WorkoutWindow workoutWindow = new WorkoutWindow(user);
                    workoutWindow.Show();
                    Application.Current.Windows[0].Close();
                }
                else
                {
                    
                    MessageBox.Show("The answer to the security question was incorrect.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
               
                Console.WriteLine($"{user.Username}: is now logged in");
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
            ForgotPassword forgotPasswordWindow = new ForgotPassword();
            forgotPasswordWindow.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
