using System;
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
            LoadWorkouts();

            //Definiera command
            NewWorkoutCommand = new RelayCommand(NewWorkout);
            WorkoutClickCommand = new RelayCommand<Workout>(WorkoutClicked);
        }

        private void LoadWorkouts()
        {
            if (Person.CurrentUser is User user)
            {
                WorkoutList = new ObservableCollection<Workout>(user.Workouts);
            }
            else if (Person.CurrentUser is AdminUser adminUser)
            {
                WorkoutList = new ObservableCollection<Workout>(adminUser.Workouts);
            }
            else
            {
                WorkoutList = new ObservableCollection<Workout>();
            }
        }

        private void NewWorkout(object parameter)
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.ShowDialog();

           //refresh
            LoadWorkouts();
        }

        private void WorkoutClicked(Workout workout)
        {
            if (workout != null)
            {
                Console.WriteLine($"Workout clicked: {workout.Type}");
                WorkoutDetailsWindow detailsWindow = new WorkoutDetailsWindow(workout);
                detailsWindow.Show();
            }
            else
            {
                MessageBox.Show("Could not retrieve workout details.");
            }
        }
    }
}
