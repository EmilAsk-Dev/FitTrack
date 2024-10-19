using System.Windows;
using System.Windows.Media;
using FitTrack.User;
using FitTrack.Windows;

namespace FitTrack
{
    public partial class MainWindow : Window
    {

        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Check if username and password match using the User.SignIn method
            Person User = new User.User(username, password); // Create temp user for login
            
            

            if (User.SignIn(username, password))
            {
                // Pass the logged-in user object to WorkoutWindow
                WorkoutWindow workoutWindow = new WorkoutWindow(User);
                workoutWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Credentials");
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show(); // Show the registration window
            this.Close(); // Close the current window
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
            // Implement forgot password logic here
        }
    }
}
