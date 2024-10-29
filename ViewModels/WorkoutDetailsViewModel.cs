using FitTrack.Commands;
using FitTrack.Users;
using FitTrack.ViewModels;
using FitTrack.Workouts;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FitTrack.ViewModels
{
    public class WorkoutDetailsViewModel : BaseViewModel
    {
        private Window _workoutDetailsWindow;
        public User CurrentUser { get; private set; } //Inloggad användare
        public User TargetUser { get; private set; } // För admin ska veta från vilken user workouten ska tas bort
        private readonly ObservableCollection<Workout> _workoutList;

        private bool _isEditable;
        public bool IsEditable
        {
            get => _isEditable;
            set
            {
                _isEditable = value;
                OnPropertyChanged(nameof(IsEditable));
            }
        }

        // Representerar det aktuella träningspasset som redigeras
        private Workout _currentWorkout;
        public Workout CurrentWorkout
        {
            get => _currentWorkout;
            private set
            {
                _currentWorkout = value;
                OnPropertyChanged(nameof(CurrentWorkout));
                UpdateWorkoutDetails(); // Update details when CurrentWorkout is set
                SelectedWorkoutType = _currentWorkout?.WorkoutType; // Set selected workout type
            }
        }

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

        private int _caloriesBurned;
        public int CaloriesBurned
        {
            get => _caloriesBurned;
            set
            {
                _caloriesBurned = value;
                OnPropertyChanged(nameof(CaloriesBurned));
            }
        }

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
        public ICommand EditWorkoutCommand { get; }

        // Konstruktör
        public WorkoutDetailsViewModel(Workout workout, Window window, User currentUser, User targetUser, ObservableCollection<Workout> workoutList)
        {
            _workoutDetailsWindow = window;
            CurrentUser = currentUser;
            TargetUser = targetUser; // Assign targetUser
            _workoutList = workoutList; // Assign the ObservableCollection

            // Initiera träningspass typer
            WorkoutTypes = new ObservableCollection<string> { "cardio", "strength" };

            // Sätt initiala värden från det aktuella träningspasset
            CurrentWorkout = workout;
            UpdateWorkoutDetails();

            IsEditable = false;

            // Initiera kommandon
            SaveWorkoutCommand = new RelayCommand(SaveWorkout);
            RemoveWorkoutCommand = new RelayCommand(RemoveWorkout);
            EditWorkoutCommand = new RelayCommand(EditWorkout);
        }

        private void EditWorkout(object obj)
        {
            IsEditable = !IsEditable; // Toggle edit mode
        }

        // Metod för att uppdatera träningspassdetaljer
        private void UpdateWorkoutDetails()
        {
            if (CurrentWorkout != null)
            {
                SelectedWorkoutType = CurrentWorkout.WorkoutType;
                Duration = CurrentWorkout.Duration.ToString(@"hh\:mm");
                CaloriesBurned = CurrentWorkout.CalcCalBurned(); 
                WorkoutDate = CurrentWorkout.WorkoutDate;
            }
        }

        // Metod för att spara träningspass detaljer
        private void SaveWorkout(object obj)
        {
            // Validera att alla nödvändiga fält är ifyllda
            if (string.IsNullOrWhiteSpace(Duration) ||
                string.IsNullOrWhiteSpace(SelectedWorkoutType))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // Uppdatera detaljerna för det aktuella träningspasset
            CurrentWorkout.Duration = TimeSpan.Parse(Duration);
            CurrentWorkout.WorkoutType = SelectedWorkoutType;
            CurrentWorkout.WorkoutDate = WorkoutDate;

            // Recalculate calories burned
            CaloriesBurned = CurrentWorkout.CalcCalBurned();

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
                        // Remove the workout from the ObservableCollection
                        _workoutList.Remove(CurrentWorkout);
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
                        // Remove the workout from the ObservableCollection
                        _workoutList.Remove(CurrentWorkout);
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
