using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FitTrack.Users;
using FitTrack.Workouts;

namespace FitTrack.Windows
{
    public partial class Workouts : Page
    {
        public List<Workout> WorkoutList { get; set; }

        public Workouts()
        {
            InitializeComponent();
            Console.WriteLine("I'm in Workout");

            
            if (Person.CurrentUser is User user)
            {
                WorkoutList = user.Workouts;
            }
            else if (Person.CurrentUser is AdminUser adminUser)
            {
                WorkoutList = adminUser.Workouts;
            }
            else
            {
                WorkoutList = new List<Workout>();
            }

            this.DataContext = this;
            workoutsControl.ItemsSource = WorkoutList;
        }

        private void NewWorkout(object sender, RoutedEventArgs e)
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.ShowDialog();

            // After the workout is added, refresh the list
            if (Person.CurrentUser is User user)
            {
                WorkoutList = user.Workouts;
            }
            else if (Person.CurrentUser is AdminUser adminUser)
            {
                WorkoutList = adminUser.Workouts;
            }
            else
            {
                WorkoutList = new List<Workout>();
            }

            workoutsControl.ItemsSource = WorkoutList;
        }

        private void WorkoutsControl_ItemClick(object sender, MouseButtonEventArgs e)
        {
            var clickedBorder = sender as Border;
            var clickedWorkout = clickedBorder?.DataContext as Workout;

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
