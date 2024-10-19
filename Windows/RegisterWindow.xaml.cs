using System.Windows;
using System.Windows.Media;
using FitTrack.User;

namespace FitTrack.Windows
{
    public partial class RegisterWindow : Window
    {



        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string Confirm = ConfirmPassword.Password;


            try
            {
                if (password == Confirm)
                {                   
                    Person person = new User.User(username, password);
                    
                    person.RegisterUser(username, password);
                    
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {                    
                    MessageBox.Show("Passwords do not match. Please try again.");
                }
            }
            catch (Exception ex) // Catching general exceptions
            {
               
                Console.WriteLine(ex.Message);                
                MessageBox.Show("An error occurred while registering the user. Please try again.");
            }







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

        private void GotoLogin(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
