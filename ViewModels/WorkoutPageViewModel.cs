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

           
            NewWorkoutCommand = new RelayCommand(NewWorkout);
            WorkoutClickCommand = new RelayCommand<Workout>(WorkoutClicked);
        }

        private void LoadWorkouts()
        {
            if (ManageUser.currentUser is User user)
            {
               
                WorkoutList.Clear();
                foreach (var workout in user.Workouts)
                {
                    Console.WriteLine($"Workout Name: {workout.WorkoutName}");
                    WorkoutList.Add(workout);
                }
            }
            else if (ManageUser.currentUser is AdminUser adminUser)
            {
               
                List<User> allUsers = ManageUser.GetAllUsers(); 
                var allWorkouts = AdminUser.ManageAllWorkouts(allUsers);

                WorkoutList.Clear();
                foreach (var workout in allWorkouts)
                {
                    WorkoutList.Add(workout);
                }
            }
            else
            {
                WorkoutList.Clear();
            }
        }


        private void NewWorkout(object parameter)
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.ShowDialog();

            LoadWorkouts();  
        }

        private void WorkoutClicked(Workout workout)
        {
            if (workout != null)
            {
                
                MessageBox.Show($"You clicked on workout: {workout.WorkoutName}"); 
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
