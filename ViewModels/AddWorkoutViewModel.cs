using FitTrack.Commands;
using FitTrack.Users;
using FitTrack.Workouts;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModels
{
    public class AddWorkoutViewModel : INotifyPropertyChanged
    {
        private string _selectedWorkoutType;
        private int _duration;
        private int _distance;
        private string _notes;
        private DateTime _workoutDate = DateTime.Now;
        private int _repetitions;

        public string SelectedWorkoutType
        {
            get => _selectedWorkoutType;
            set
            {
                _selectedWorkoutType = value;
                OnPropertyChanged(nameof(SelectedWorkoutType));
                OnPropertyChanged(nameof(IsDistanceVisible));
                OnPropertyChanged(nameof(IsRepetitionsVisible));
            }
        }

        public int Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        public int Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }

        public string Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        public DateTime WorkoutDate
        {
            get => _workoutDate;
            set
            {
                _workoutDate = value;
                OnPropertyChanged(nameof(WorkoutDate));
            }
        }

        public int Repetitions
        {
            get => _repetitions;
            set
            {
                _repetitions = value;
                OnPropertyChanged(nameof(Repetitions));
            }
        }

        public bool IsDistanceVisible => SelectedWorkoutType == "Cardio";
        public bool IsRepetitionsVisible => SelectedWorkoutType == "Strength";

        public ICommand SaveWorkoutCommand { get; }

        public AddWorkoutViewModel()
        {
            
            SaveWorkoutCommand = new RelayCommand(_ => SaveWorkout());
        }

        private void SaveWorkout()
        {
            if (string.IsNullOrEmpty(SelectedWorkoutType) || Duration <= 0)
            {
                MessageBox.Show("Please provide valid workout details.");
                return;
            }

            Workout workout;

            if (SelectedWorkoutType == "Cardio")
            {
                workout = new CardioWorkout(WorkoutDate, SelectedWorkoutType, TimeSpan.FromMinutes(Duration), 0, Notes, Distance);
            }
            else if (SelectedWorkoutType == "Strength")
            {
                workout = new StrenghtWorkout(WorkoutDate, SelectedWorkoutType, TimeSpan.FromMinutes(Duration), 0, Notes);
            }
            else
            {
                MessageBox.Show("Invalid workout type.");
                return;
            }

          
            if (Person.CurrentUser is User user)
            {
                user.Workouts.Add(workout);
            }
            else if (Person.CurrentUser is AdminUser adminUser)
            {
                adminUser.Workouts.Add(workout);
            }
            else
            {
                MessageBox.Show("User not found or invalid.");
                return;
            }

            MessageBox.Show("Workout saved successfully!");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
