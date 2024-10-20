using FitTrack.User;
using System.Windows;

namespace FitTrack.Windows
{
    public partial class WorkoutWindow : Window
    {


        public WorkoutWindow(User.User user)  
        {
            InitializeComponent();
            Console.WriteLine("Im in WorkoutWIndow");


            WelcomeUser.Text = "Välkommen " + user.Username;  

            
            ContentFrame.Navigate(new HomePage());
        }


        private void Home_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new HomePage());
        }

        
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        
        private void WorkoutsButton_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new Workouts());
        }
    }
}
