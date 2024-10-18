using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FitTrack.User;
using FitTrack.Windows;


namespace FitTrack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private FitTrack.User.User _user;
        private FitTrack.User.AdminUser _admin;


        public MainWindow()
        {
            InitializeComponent();
            _user = new FitTrack.User.User("User", "1234", "Sweden");
            _admin = new FitTrack.User.AdminUser("Admin", "1234");
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
           
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            
            bool IsLoggedIn = _user.SignIn(username, password);

            if (IsLoggedIn)
            {                
                WorkoutWindow workoutWindow = new WorkoutWindow();
                workoutWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Credentials");
            }
                





        }

        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            this.Close();
            registerWindow.Show();
        }

        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "Username")
            {
                UsernameTextBox.Text = "";
                UsernameTextBox.Foreground = new SolidColorBrush(System.Windows.Media.Colors.White);
            }
        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                UsernameTextBox.Text = "Username";
                UsernameTextBox.Foreground = new SolidColorBrush(System.Windows.Media.Colors.Gray);
            }
        }

       
    }
}