using System.Collections.Generic;
using System.Windows.Controls;

namespace FitTrack.Windows
{
    public partial class Workouts : Page
    {

        public List<string> WorkoutList { get; set; }


        public Workouts()
        {
            InitializeComponent();
            Console.WriteLine("I'm in Workout");

            
            WorkoutList = User.User.CurrentUser?.Workouts ?? new List<string>(); 

            
            workoutsControl.ItemsSource = WorkoutList;
        }

        private void NewWorkout(object sender, System.Windows.RoutedEventArgs e)
        {
            
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.Show();  
        }
    }
}
