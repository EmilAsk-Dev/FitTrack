using FitTrack.Commands;
using FitTrack.Users;
using FitTrack.Workouts;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using FitTrack;

public class AddWorkoutViewModel : INotifyPropertyChanged
{
    private ComboBoxItem _selectedWorkoutType; // Ändra till ComboBoxItem
    private int _duration; // Varaktighet av träning
    private int _distance; // Avstånd för träning
    private string _notes; // Anteckningar för träning
    private DateTime _workoutDate = DateTime.Now; // Datum för träning
    private int _repetitions; // Antal repetitioner

    // Synlighetsegenskaper
    private bool _isDistanceVisible; // Visar avståndsfältet
    private bool _isRepetitionsVisible; // Visar repetitionsfältet

    public ComboBoxItem SelectedWorkoutType
    {
        get => _selectedWorkoutType;
        set
        {
            if (_selectedWorkoutType != value)
            {
                _selectedWorkoutType = value;
                OnPropertyChanged(nameof(SelectedWorkoutType));

                // Uppdatera synlighet baserat på vald träningstyp
                UpdateVisibility();
            }
        }
    }

    public int Duration
    {
        get => _duration;
        set
        {
            if (_duration != value)
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }
    }

    public int Distance
    {
        get => _distance;
        set
        {
            if (_distance != value)
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }
    }

    public string Notes
    {
        get => _notes;
        set
        {
            if (_notes != value)
            {
                _notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }
    }

    public DateTime WorkoutDate
    {
        get => _workoutDate;
        set
        {
            if (_workoutDate != value)
            {
                _workoutDate = value;
                OnPropertyChanged(nameof(WorkoutDate));
            }
        }
    }

    public int Repetitions
    {
        get => _repetitions;
        set
        {
            if (_repetitions != value)
            {
                _repetitions = value;
                OnPropertyChanged(nameof(Repetitions));
            }
        }
    }

    public bool IsDistanceVisible
    {
        get => _isDistanceVisible;
        private set
        {
            if (_isDistanceVisible != value)
            {
                _isDistanceVisible = value;
                OnPropertyChanged(nameof(IsDistanceVisible));
            }
        }
    }

    public bool IsRepetitionsVisible
    {
        get => _isRepetitionsVisible;
        private set
        {
            if (_isRepetitionsVisible != value)
            {
                _isRepetitionsVisible = value;
                OnPropertyChanged(nameof(IsRepetitionsVisible));
            }
        }
    }

    public ICommand SaveWorkoutCommand { get; }

    public AddWorkoutViewModel()
    {
        SaveWorkoutCommand = new RelayCommand(_ => SaveWorkout());
        UpdateVisibility(); // Initial synlighetsinställning
    }

    private void UpdateVisibility()
    {
        // Kontrollera vald träningstyp
        if (SelectedWorkoutType?.Content.ToString() == "Cardio")
        {
            IsDistanceVisible = true; // Visa avstånd
            IsRepetitionsVisible = false; // Dölja repetitioner
        }
        else if (SelectedWorkoutType?.Content.ToString() == "Strength")
        {
            IsDistanceVisible = false; // Dölja avstånd
            IsRepetitionsVisible = true; // Visa repetitioner
        }
        else
        {
            IsDistanceVisible = false; // Dölja avstånd
            IsRepetitionsVisible = false; // Dölja repetitioner
        }
    }

    private void SaveWorkout()
    {
        if (SelectedWorkoutType == null || Duration <= 0)
        {
            MessageBox.Show("Please provide valid workout details."); // Felmeddelande för ogiltiga uppgifter
            return;
        }

        Workout workout;

        // Kontrollera vald träningstyp
        if (SelectedWorkoutType.Content.ToString() == "Cardio")
        {
            workout = new CardioWorkout(WorkoutDate, SelectedWorkoutType.Content.ToString(), TimeSpan.FromMinutes(Duration), 0, Notes, Distance);
        }
        else if (SelectedWorkoutType.Content.ToString() == "Strength")
        {
            workout = new StrenghtWorkout(WorkoutDate, SelectedWorkoutType.Content.ToString(), TimeSpan.FromMinutes(Duration), 0, Notes, Repetitions);
        }
        else
        {
            MessageBox.Show("Invalid workout type."); // Felmeddelande för ogiltig träningstyp
            return;
        }

        User user = ManageUser.currentUser;

        if (user != null)
        {
            user.Workouts.Add(workout); // Lägg till träning för användare
        }        
        else
        {
            MessageBox.Show("User not found or invalid."); // Felmeddelande för ogiltig användare
            return;
        }

        MessageBox.Show("Workout saved successfully!"); // Meddelande för framgång
        
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Uträtta egenskapändringar
    }
}
