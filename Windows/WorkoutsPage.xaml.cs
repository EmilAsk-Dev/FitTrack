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
            Console.WriteLine("Im in Workout");


            WorkoutList = new List<string>
            {
                "Yoga",
                "Cardio",
                "Strength Training",
                "Pilates",
                "HIIT",
                "Bollar",
                "Maskar"
            };

            

            
            workoutsControl.ItemsSource = WorkoutList;

            
        }

        private void NewWorkout(object sender, System.Windows.RoutedEventArgs e)
        {
            // Ensure you use the correct method to show the window
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.Show();  // Correct method to open the window
        }
    }
}
