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
                //OnPropertyChanged();
            }
        }

        private string _calBurned;
        public string CalBurned
        {
            get => _calBurned;
            set
            {
                _calBurned = value;
                //OnPropertyChanged();
            }
        }

        public ICommand SaveWorkoutCommand { get; set; }
        public ICommand RemoveWorkoutCommand { get; set; }

        public WorkoutDetailsViewModel(Workout workout)
        {
            CurrentWorkout = workout;

            
            WorkoutTypes = new ObservableCollection<string> { "Cardio", "Strength" };
            Duration = CurrentWorkout.Duration.ToString(@"hh\:mm");
            CalBurned = CurrentWorkout.CalBurned.ToString();

            SaveWorkoutCommand = new RelayCommand(SaveWorkout);
            RemoveWorkoutCommand = new RelayCommand(RemoveWorkout);
        }

        private void SaveWorkout(object obj)
        {
            if (string.IsNullOrWhiteSpace(Duration) || string.IsNullOrWhiteSpace(CalBurned) || CurrentWorkout.Date == null || CurrentWorkout.Type == null)
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            
            CurrentWorkout.Duration = TimeSpan.Parse(Duration);
            CurrentWorkout.CalBurned = int.Parse(CalBurned);

            
            Application.Current.Windows[0]?.Close();
        }

        private void RemoveWorkout(object obj)
        {
            

        }
    }
}
