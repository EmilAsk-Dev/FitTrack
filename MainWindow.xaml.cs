using System.Windows;
using System.Windows.Media;
using FitTrack.Users;
using FitTrack.Windows;

namespace FitTrack
{
    public partial class MainWindow : Window
    {

        

        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("I'm in Main");
            AddUsersAndWorkouts.AddUsersAndWorkoutsToDatabase();


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


            User user = new User(username, password); 

            
            if (user.SignIn(username, password))
            {
                WorkoutWindow workoutWindow = new WorkoutWindow(user); 
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
