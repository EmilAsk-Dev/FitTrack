using FitTrack.Commands;
using FitTrack.Users;
using FitTrack.ViewModels;
using FitTrack.Workouts;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModels
{
    public class WorkoutDetailsViewModel : BaseViewModel
    {
        private Window _workoutDetailsWindow;
        public User CurrentUser { get; private set; } // The user currently logged in
        public User TargetUser { get; private set; } // The user whose workout is being managed

        // Representerar det aktuella träningspasset som redigeras
        public Workout CurrentWorkout { get; private set; }

        // Observable-kollektion för träningspass typer
        private ObservableCollection<string> _workoutTypes;
        public ObservableCollection<string> WorkoutTypes
        {
            get => _workoutTypes;
            private set
            {
                _workoutTypes = value;
                OnPropertyChanged(nameof(WorkoutTypes));
            }
        }

        // Egenskap för valt träningspass typ
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

        // Egenskaper för varaktighet och kalorier förbrända
        public string Duration { get; set; }
        public string CalBurned { get; set; }

        // Egenskap för träningsdatum
        private DateTime _workoutDate;
        public DateTime WorkoutDate
        {
            get => _workoutDate;
            set
            {
                _workoutDate = value;
                OnPropertyChanged(nameof(WorkoutDate));
            }
        }

        // Kommandon för att spara och ta bort träningspass
        public ICommand SaveWorkoutCommand { get; }
        public ICommand RemoveWorkoutCommand { get; }

        // Konstruktör
        public WorkoutDetailsViewModel(Workout workout, Window window, User currentUser, User targetUser)
        {
            CurrentWorkout = workout;
            _workoutDetailsWindow = window;
            CurrentUser = currentUser;
            TargetUser = targetUser; // Assign targetUser

            // Initiera träningspass typer
            WorkoutTypes = new ObservableCollection<string> { "Cardio", "Strength" };

            // Sätt initiala värden från det aktuella träningspasset
            SelectedWorkoutType = CurrentWorkout.WorkoutType;
            Duration = CurrentWorkout.Duration.ToString(@"hh\:mm");
            CalBurned = CurrentWorkout.CaloriesBurned.ToString();
            WorkoutDate = CurrentWorkout.WorkoutDate;

            // Initiera kommandon
            SaveWorkoutCommand = new RelayCommand(SaveWorkout);
            RemoveWorkoutCommand = new RelayCommand(RemoveWorkout);
        }

        // Metod för att spara träningspass detaljer
        private void SaveWorkout(object obj)
        {
            // Validera att alla nödvändiga fält är ifyllda
            if (string.IsNullOrWhiteSpace(Duration) ||
                string.IsNullOrWhiteSpace(CalBurned) ||
                string.IsNullOrWhiteSpace(SelectedWorkoutType))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Uppdatera detaljerna för det aktuella träningspasset
            CurrentWorkout.Duration = TimeSpan.Parse(Duration);
            CurrentWorkout.CaloriesBurned = int.Parse(CalBurned);
            CurrentWorkout.WorkoutType = SelectedWorkoutType;
            CurrentWorkout.WorkoutDate = WorkoutDate;

            // Stäng fönstret efter att ha sparat
            _workoutDetailsWindow?.Close();
        }

        // Metod för att ta bort träningspasset
        private void RemoveWorkout(object obj)
        {
            var result = MessageBox.Show("Are you sure you want to remove this workout?", "Confirm Removal", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                // Om den aktuella användaren är administratör, tillåt borttagning av träningspass från valfri användare
                if (CurrentUser.IsAdmin)
                {
                    // Hämta den målanvändare som äger träningspasset
                    User targetUser = ManageUser.GetUserByWorkout(CurrentWorkout); // Hämta målanvändaren via träningspasset

                    if (targetUser != null)
                    {
                        // Ta bort träningspasset från målanvändaren
                        ManageUser.RemoveWorkoutFromUser(CurrentWorkout, targetUser);
                    }
                    else
                    {
                        MessageBox.Show("Target user not found.");
                    }
                }
                else
                {
                    // Ta bort endast om det är den aktuella användarens träningspass
                    if (ManageUser.currentUser.Workouts.Contains(CurrentWorkout))
                    {
                        ManageUser.currentUser.Workouts.Remove(CurrentWorkout);
                    }
                    else
                    {
                        MessageBox.Show("You can only remove your own workouts.");
                    }
                }

                _workoutDetailsWindow?.Close(); // Stäng detaljerfönstret efter borttagning
            }
        }
    }
}
