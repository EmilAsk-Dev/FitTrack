using FitTrack.Commands;
using FitTrack.ViewModels;
using FitTrack.Workouts;
using FitTrack;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

public class WorkoutDetailsViewModel : BaseViewModel
{
    private Window _workoutDetailsWindow;

    public Workout CurrentWorkout { get; private set; }

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

    public string Duration { get; set; }
    public string CalBurned { get; set; }

    
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

    public ICommand SaveWorkoutCommand { get; }
    public ICommand RemoveWorkoutCommand { get; }

    public WorkoutDetailsViewModel(Workout workout, Window window)
    {
        CurrentWorkout = workout;
        _workoutDetailsWindow = window; // Assign the window parameter to the private field

        WorkoutTypes = new ObservableCollection<string> { "Cardio", "Strength" };

        SelectedWorkoutType = CurrentWorkout.WorkoutType;  // Set the selected workout type
        Duration = CurrentWorkout.Duration.ToString(@"hh\:mm");
        CalBurned = CurrentWorkout.CaloriesBurned.ToString();

        // Set the workout date from the current workout
        WorkoutDate = CurrentWorkout.WorkoutDate; // Make sure WorkoutDate is set

        SaveWorkoutCommand = new RelayCommand(SaveWorkout);
        RemoveWorkoutCommand = new RelayCommand(RemoveWorkout);
    }

    private void SaveWorkout(object obj)
    {
        // Validate that all required fields are filled
        if (string.IsNullOrWhiteSpace(Duration) ||
            string.IsNullOrWhiteSpace(CalBurned) ||
            string.IsNullOrWhiteSpace(SelectedWorkoutType))
        {
            MessageBox.Show("Please fill in all required fields.");
            return;
        }

        CurrentWorkout.Duration = TimeSpan.Parse(Duration);
        CurrentWorkout.CaloriesBurned = int.Parse(CalBurned);
        CurrentWorkout.WorkoutType = SelectedWorkoutType;
        CurrentWorkout.WorkoutDate = WorkoutDate; // Update WorkoutDate
        CurrentWorkout.Notes = CurrentWorkout.Notes;

        _workoutDetailsWindow?.Close();
    }

    private void RemoveWorkout(object obj)
    {
        var result = MessageBox.Show("Are you sure you want to remove this workout?", "Confirm Removal", MessageBoxButton.YesNo);

        if (result == MessageBoxResult.Yes)
        {
            ManageUser.currentUser.Workouts.Remove(CurrentWorkout);
            _workoutDetailsWindow?.Close();
        }
    }
}
