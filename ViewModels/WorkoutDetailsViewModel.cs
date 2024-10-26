using FitTrack.Commands;
using FitTrack.ViewModels;
using FitTrack.Workouts;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.Windows
{
    public class WorkoutDetailsViewModel : BaseViewModel
    {
        public Workout CurrentWorkout { get; set; }

        public ObservableCollection<string> WorkoutTypes { get; set; }

        private string _duration;
        public string Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                OnPropertyChanged();  
            }
        }

        private string _calBurned;
        public string CalBurned
        {
            get => _calBurned;
            set
            {
                _calBurned = value;
                OnPropertyChanged();  
            }
        }

        private string _selectedWorkoutType;
        public string SelectedWorkoutType
        {
            get => _selectedWorkoutType;
            set
            {
                _selectedWorkoutType = value;
                OnPropertyChanged(nameof(SelectedWorkoutType));
            }
        }

        public ICommand SaveWorkoutCommand { get; set; }
        public ICommand RemoveWorkoutCommand { get; set; }

        public WorkoutDetailsViewModel(Workout workout)
        {
            CurrentWorkout = workout;

            // Initialize WorkoutTypes collection
            WorkoutTypes = new ObservableCollection<string> { "Cardio", "Strength" };

            // Set the selected workout type
            SelectedWorkoutType = CurrentWorkout.WorkoutType;  // Ensure this is set to the workout type

            Duration = CurrentWorkout.Duration.ToString(@"hh\:mm");
            CalBurned = CurrentWorkout.CaloriesBurned.ToString();

            SaveWorkoutCommand = new RelayCommand(SaveWorkout);
            RemoveWorkoutCommand = new RelayCommand(RemoveWorkout);
        }

        private void SaveWorkout(object obj)
        {
            if (string.IsNullOrWhiteSpace(Duration) || string.IsNullOrWhiteSpace(CalBurned) || CurrentWorkout.WorkoutType == null || CurrentWorkout.WorkoutType == null)
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Save logic can be implemented here

            Application.Current.Windows[0]?.Close();
        }

        private void RemoveWorkout(object obj)
        {
            // Logic for removing the workout can be implemented here
        }
    }
}
