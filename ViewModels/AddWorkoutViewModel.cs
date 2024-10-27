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
    // Fält för att hålla information om det valda träningsalternativet
    private ComboBoxItem _selectedWorkoutType;
    private int _duration; // Lagrar träningens varaktighet i minuter
    private int _distance; // Lagrar avstånd för konditionsträning i meter eller kilometer
    private string _notes; // Anteckningar om träningen
    private DateTime _workoutDate = DateTime.Now; // Datum då träningen genomfördes
    private int _repetitions; // Antal repetitioner för styrketräning
    private string _workoutName; // Namn på träningspasset
    private bool _isDistanceVisible; // Synlighet för avståndsfältet
    private bool _isRepetitionsVisible; // Synlighet för repetitionsfältet

    // Egenskap för att ange och hämta träningspassets namn
    public string WorkoutName
    {
        get => _workoutName;
        set
        {
            if (_workoutName != value)
            {
                _workoutName = value;
                OnPropertyChanged(nameof(WorkoutName));
            }
        }
    }

    // Egenskap för att ange och hämta vald träningskategori
    public ComboBoxItem SelectedWorkoutType
    {
        get => _selectedWorkoutType;
        set
        {
            if (_selectedWorkoutType != value)
            {
                _selectedWorkoutType = value;
                OnPropertyChanged(nameof(SelectedWorkoutType));
                UpdateVisibility(); // Uppdaterar synlighet baserat på valt träningsalternativ
            }
        }
    }

    // Egenskap för att hantera träningens varaktighet
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

    // Egenskap för att hantera avståndet
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

    // Egenskap för att hantera anteckningar om träningen
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

    // Egenskap för att ange och hämta datumet för träningen
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

    // Egenskap för att hantera antalet repetitioner för styrketräning
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

    // Egenskap som styr om avståndsfältet ska visas eller inte
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

    // Egenskap som styr om repetitionsfältet ska visas eller inte
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

    // Kommando för att spara träningspass
    public ICommand SaveWorkoutCommand { get; }

    // Konstruktor för att initiera kommandot och uppdatera synligheten
    public AddWorkoutViewModel()
    {
        SaveWorkoutCommand = new RelayCommand(_ => SaveWorkout());
        UpdateVisibility(); // Ställer in synlighet vid initiering
    }

    // Metod för att uppdatera synligheten av avstånd och repetitioner beroende på träningskategori
    private void UpdateVisibility()
    {
        if (SelectedWorkoutType?.Content?.ToString() == "Cardio")
        {
            IsDistanceVisible = true;
            IsRepetitionsVisible = false;
        }
        else if (SelectedWorkoutType?.Content?.ToString() == "Strength")
        {
            IsDistanceVisible = false;
            IsRepetitionsVisible = true;
        }
        else
        {
            IsDistanceVisible = false;
            IsRepetitionsVisible = false;
        }
    }

    // Metod för att spara träningspasset med validering av inmatade värden
    private void SaveWorkout()
    {
        if (SelectedWorkoutType == null || Duration <= 0)
        {
            MessageBox.Show("Please provide valid workout details.");
            return;
        }

        Workout workout;
        string workoutType = SelectedWorkoutType.Content.ToString();

        // Skapa träningspass beroende på om det är kondition eller styrka
        if (workoutType == "Cardio" && Distance > 0)
        {
            workout = new CardioWorkout(WorkoutName, WorkoutDate, workoutType, TimeSpan.FromMinutes(Duration), 0, Notes, Distance);
        }
        else if (workoutType == "Strength" && Repetitions > 0)
        {
            workout = new StrenghtWorkout(WorkoutName, WorkoutDate, workoutType, TimeSpan.FromMinutes(Duration), 0, Notes, Repetitions);
        }
        else
        {
            MessageBox.Show("Please provide valid values for the workout type.");
            return;
        }

        // Kontrollera om aktuell användare är giltig innan träningspasset sparas
        if (ManageUser.currentUser != null)
        {
            ManageUser.currentUser.Workouts.Add(workout);
            MessageBox.Show("Workout saved successfully!");
        }
        else
        {
            MessageBox.Show("User not found or invalid.");
        }
    }

    // Händelse för att hantera förändringar av egenskaper
    public event PropertyChangedEventHandler PropertyChanged;

    // Metod för att meddela att en egenskap har ändrats
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
