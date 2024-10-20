using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FitTrack.Windows
{
    public partial class Workouts : Page
    {
        public List<Workout.Workout> WorkoutList { get; set; }

        public Workouts()
        {
            InitializeComponent();
            Console.WriteLine("I'm in Workout");

            
            WorkoutList = User.User.CurrentUser?.Workouts ?? new List<Workout.Workout>();            
            this.DataContext = this;            
            workoutsControl.ItemsSource = WorkoutList;
        }

        private void NewWorkout(object sender, RoutedEventArgs e)
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.Show();
        }

        private void WorkoutsControl_ItemClick(object sender, MouseButtonEventArgs e)
        {
            
            var clickedBorder = sender as Border;
            var clickedWorkout = clickedBorder?.DataContext as Workout.Workout;

            if (clickedWorkout != null)
            {
                Console.WriteLine($"Workout clicked: {clickedWorkout.Type}");
                WorkoutDetailsWindow detailsWindow = new WorkoutDetailsWindow(clickedWorkout);
                detailsWindow.Show();
            }
            else
            {
                MessageBox.Show("Could not retrieve workout details.");
            }
        }
    }
}
