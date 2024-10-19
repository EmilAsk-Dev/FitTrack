using FitTrack.User; // Ensure correct usage of User class
using System.Windows;

namespace FitTrack.Windows
{
    public partial class WorkoutWindow : Window
    {
        

        public WorkoutWindow(User.User User) 
        {
            InitializeComponent();
            

            
            ContentFrame.Navigate(new HomePage()); 
        }

       
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new HomePage());
        }

        // Handle log out
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
