using System.Windows;
using System.Windows.Controls;

namespace FitTrack.Windows
{
    public partial class WorkoutWindow : Window
    {
        public List<string> WorkoutList { get; set; }

        public WorkoutWindow()
        {
            InitializeComponent();

            //placeHolders
            WorkoutList = new List<string>
            {
                "Yoga",
                "Cardio",
                "Strength Training",
                "Pilates",
                "HIIT",
                "bollar",
                "maskar"
            };

            // binder till ItemsControl
            workoutsControl.ItemsSource = WorkoutList;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            //loggar ut
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        

        private void Home_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WorkoutsButton_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Settings(object sender, RoutedEventArgs e)
        {

        }
    }
}
