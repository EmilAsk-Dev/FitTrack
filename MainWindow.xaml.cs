using System.Windows;
using System.Windows.Media;
using FitTrack.Users;
using FitTrack.Windows;

namespace FitTrack
{
    public partial class MainWindow : Window
    {

        static bool firstOpen = true;

        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("I'm in Main");
            if (firstOpen)
            {
                AddUsersAndWorkouts.AddUsersAndWorkoutsToDatabase();
                firstOpen = false;
            }            
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if(Person.CheckPassword(password) != string.Empty)
            {
                MessageBox.Show("Invalid Credentials");
                return;
            }

            Person user = Person.FindUser(username);
            
            if(user != null)
            {
                bool SignedIn = user.SignIn(username, password);
                WorkoutWindow workoutWindow = new WorkoutWindow(user);
                workoutWindow.Show();
                this.Close();
            }
            
                        

            
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show(); 
            this.Close(); 
        }

        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "Username")
            {
                UsernameTextBox.Text = "";
                UsernameTextBox.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                UsernameTextBox.Text = "Username";
                UsernameTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {

            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.Show();
            this.Close();
        }
    }
}



