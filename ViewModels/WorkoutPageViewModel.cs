using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using FitTrack.Users;
using FitTrack.Workouts;
using FitTrack.Commands;
using FitTrack.ViewModels;

namespace FitTrack.Windows
{
    public class WorkoutPageViewModel : BaseViewModel
    {
        public ObservableCollection<Workout> WorkoutList { get; set; }
        private Workout _selectedWorkout;

        public Workout SelectedWorkout
        {
            get => _selectedWorkout;
            set
            {
                _selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));

                if (_selectedWorkout != null)
                {
                    WorkoutClickCommand.Execute(_selectedWorkout);
                }
            }
        }

        public ICommand NewWorkoutCommand { get; }
        public ICommand WorkoutClickCommand { get; }

        public WorkoutPageViewModel()
        {
            WorkoutList = new ObservableCollection<Workout>();
            LoadWorkouts();

            // Define commands
            NewWorkoutCommand = new RelayCommand(NewWorkout);
            WorkoutClickCommand = new RelayCommand<Workout>(WorkoutClicked);
        }

        private void LoadWorkouts()
        {
            // Check the current user and load their workouts
            if (ManageUser.currentUser is User user)
            {
                WorkoutList.Clear();  // Clear existing workouts to avoid duplicates
                foreach (var workout in user.Workouts)
                {
                    Console.WriteLine($"Workout Type: {workout.WorkoutType}");
                    WorkoutList.Add(workout);  // Add each workout to the ObservableCollection
                }
            }
            else if (ManageUser.currentUser is AdminUser adminUser)
            {
                WorkoutList.Clear();  // Clear existing workouts to avoid duplicates
                foreach (var workout in adminUser.Workouts)
                {
                    WorkoutList.Add(workout);  // Add each workout to the ObservableCollection
                }
            }
            else
            {
                WorkoutList.Clear();  // Clear if no valid user is found
            }
        }

        private void NewWorkout(object parameter)
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.ShowDialog();

            LoadWorkouts();  // Reload workouts after adding a new one
        }

        private void WorkoutClicked(Workout workout)
        {
            if (workout != null)
            {
                // Handle the click, e.g., show details or navigate to a workout detail page
                MessageBox.Show($"You clicked on workout: {workout.WorkoutType}");
                WorkoutDetailsWindow workoutDetailsWindow = new WorkoutDetailsWindow(workout);
                workoutDetailsWindow.Show();

            }
            else
            {
                MessageBox.Show("Could not retrieve workout details.");
            }
        }
    }
}
