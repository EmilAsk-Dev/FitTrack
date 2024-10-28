using FitTrack.Commands;
using FitTrack.Users;
using FitTrack.ViewModels;
using FitTrack.Workouts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.Windows
{
    // Den här klassen är ViewModel för WorkoutPage, ansvarar för logiken bakom sidan
    public class WorkoutPageViewModel : BaseViewModel
    {
        // Samling av träningspass som visas på sidan
        public ObservableCollection<Workout> WorkoutList { get; set; }
        // Lista för att lagra alla träningspass, så att vi kan filtrera
        private List<Workout> _allWorkouts;

        // Den valda träningen
        private Workout _selectedWorkout;
        public Workout SelectedWorkout
        {
            get => _selectedWorkout;
            set
            {
                _selectedWorkout = value; // Sätt den valda träningen
                OnPropertyChanged(nameof(SelectedWorkout)); // Uppdatera visningen
                if (_selectedWorkout != null)
                {
                    WorkoutClickCommand.Execute(_selectedWorkout); // Utför kommandot om det finns en vald träning
                }
            }
        }

        // Filter för träningsnamn
        private string _workoutNameFilter;
        public string WorkoutNameFilter
        {
            get => _workoutNameFilter;
            set
            {
                _workoutNameFilter = value; // Sätt filtervärdet
                OnPropertyChanged(nameof(WorkoutNameFilter)); // Uppdatera visningen
                ApplyFilter(); // Tillämpa filter
            }
        }

        // Filter för träningstyp
        private string _workoutTypeFilter;
        public string WorkoutTypeFilter
        {
            get => _workoutTypeFilter;
            set
            {
                // Extrahera den sista delen av strängen, t.ex., "Cardio" från "System.Windows.Controls.ComboBoxItem: Cardio"
                if (!string.IsNullOrEmpty(value))
                {
                    var parts = value.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 1)
                    {
                        _workoutTypeFilter = parts[1].Trim(); // Ta bort mellanslag
                    }
                    else
                    {
                        _workoutTypeFilter = value.Trim(); // Använd originalvärdet om ingen ':' hittades
                    }
                }
                else
                {
                    _workoutTypeFilter = null; // Sätt till null om det är tomt
                }

                OnPropertyChanged(nameof(WorkoutTypeFilter)); // Uppdatera visningen
                ApplyFilter(); // Tillämpa filter
            }
        }

        // Filter för varaktighet
        private int? _durationFilter;
        public int? DurationFilter
        {
            get => _durationFilter;
            set
            {
                _durationFilter = value; // Sätt filtervärdet
                OnPropertyChanged(nameof(DurationFilter)); // Uppdatera visningen
                ApplyFilter(); // Tillämpa filter
            }
        }

        // Sorteringsalternativ
        private string _sortBy;
        public string SortBy
        {
            get => _sortBy;
            set
            {
                // Extrahera den sista delen av strängen, t.ex., "Most Recent" från "System.Windows.Controls.ComboBoxItem: Most Recent"
                if (!string.IsNullOrEmpty(value))
                {
                    var parts = value.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 1)
                    {
                        _sortBy = parts[1].Trim(); // Ta bort mellanslag
                    }
                    else
                    {
                        _sortBy = value.Trim(); // Använd originalvärdet om ingen ':' hittades
                    }
                }
                else
                {
                    _sortBy = null; // Sätt till null om det är tomt
                }

                OnPropertyChanged(nameof(SortBy)); // Uppdatera visningen
                ApplyFilter(); // Tillämpa filter
            }
        }
        
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                LoadUserWorkouts(); 
            }
        }

        private User _currentUser;

        // Egenskap för att få eller sätta den aktuella användaren
        public User CurrentUser
        {
            get => _currentUser; // Returnera det aktuella User-objektet
            set
            {
                if (_currentUser != value)
                {
                    _currentUser = value;
                    OnPropertyChanged(nameof(CurrentUser));
                    // Meddela att IsAdmin kan ha ändrats när CurrentUser ändras
                    OnPropertyChanged(nameof(IsAdmin));
                }
            }
        }

        // Egenskap för att avgöra om den aktuella användaren är en administratör
        public bool IsAdmin => ManageUser.currentUser?.IsAdmin ?? false; // Säker kontroll av IsAdmin

        // Kommandon för att skapa nytt träningspass och klicka på ett träningspass
        public ICommand NewWorkoutCommand { get; }
        public ICommand WorkoutClickCommand { get; }
        

        // Konstruktorn
        public WorkoutPageViewModel()
        {            

            Users = new ObservableCollection<User>(ManageUser.GetAllUsers());

            WorkoutList = new ObservableCollection<Workout>(); // Initiera WorkoutList
            _allWorkouts = new List<Workout>(); // Initiera _allWorkouts
            LoadWorkouts(); // Ladda alla träningspass

            NewWorkoutCommand = new RelayCommand(NewWorkout); // Initiera kommandon
            WorkoutClickCommand = new RelayCommand<Workout>(WorkoutClicked);

            SortBy = null; // Standardvärde för sortering
            WorkoutTypeFilter = null; // Standardvärde för träningstyp
        }


        // Ladda alla träningspass beroende på användartyp (vanlig användare eller admin)
        private void LoadWorkouts()
        {
            if (ManageUser.currentUser is User user) // Om användaren är en vanlig användare
            {
                _allWorkouts = user.Workouts.ToList(); // Hämta träningspass
            }
            else if (ManageUser.currentUser is AdminUser adminUser) // Om användaren är en admin
            {
                List<User> allUsers = ManageUser.GetAllUsers(); // Hämta alla användare
                _allWorkouts = AdminUser.ManageAllWorkouts(allUsers); // Hämta träningspass för alla användare
            }
            else
            {
                _allWorkouts.Clear(); // Rensa listan om ingen användare är inloggad
            }

            ApplyFilter(); // Tillämpa filter på de laddade träningspassen
        }

        private void LoadUserWorkouts()
        {
            if (_selectedUser != null)
            {
                // Load workouts for the selected user
                _allWorkouts = _selectedUser.Workouts.ToList();
            }
            else
            {
                // If no user is selected, load all workouts based on user type
                LoadWorkouts();
            }

            ApplyFilter(); // Apply filters after loading workouts
        }


        // Tillämpa filter och sortering på träningspassen
        private void ApplyFilter()
        {
            var filteredWorkouts = new List<Workout>(); // Lista för filtrerade träningspass

            // Filtrera efter träningsnamn
            foreach (var workout in _allWorkouts)
            {
                bool matchesFilter = true; // Anta att träningspasset matchar filter

                // Kontrollera om träningspasset matchar namnet
                if (!string.IsNullOrEmpty(WorkoutNameFilter) &&
                    workout.WorkoutName.IndexOf(WorkoutNameFilter, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    matchesFilter = false; // Matchar inte, hoppa över detta träningspass
                }

                // Kontrollera om träningspasset matchar typ
                if (!string.IsNullOrEmpty(WorkoutTypeFilter) &&
                    (string.IsNullOrEmpty(workout.WorkoutType) ||
                     !workout.WorkoutType.Equals(WorkoutTypeFilter, StringComparison.OrdinalIgnoreCase)))
                {
                    matchesFilter = false; // Matchar inte, hoppa över detta träningspass
                }

                // Kontrollera om träningspasset matchar varaktighet
                if (DurationFilter.HasValue && workout.Duration != TimeSpan.FromMinutes(DurationFilter.Value))
                {
                    matchesFilter = false; // Matchar inte, hoppa över detta träningspass
                }

                // Om alla filter matchar, lägg till i filtrerade träningspass
                if (matchesFilter)
                {
                    filteredWorkouts.Add(workout);
                }
            }

            // Tillämpa sortering
            if (!string.IsNullOrEmpty(SortBy))
            {
                // Sortera filtrerade träningspass
                for (int i = 0; i < filteredWorkouts.Count - 1; i++)
                {
                    for (int j = 0; j < filteredWorkouts.Count - i - 1; j++)
                    {
                        // Sortera efter datum (nyast först)
                        if (SortBy.Equals("Most Recent", StringComparison.OrdinalIgnoreCase) &&
                            filteredWorkouts[j].WorkoutDate < filteredWorkouts[j + 1].WorkoutDate)
                        {
                            // Byt plats
                            var temp = filteredWorkouts[j];
                            filteredWorkouts[j] = filteredWorkouts[j + 1];
                            filteredWorkouts[j + 1] = temp;
                        }
                        // Sortera efter datum (äldst först)
                        else if (SortBy.Equals("Oldest", StringComparison.OrdinalIgnoreCase) &&
                                 filteredWorkouts[j].WorkoutDate > filteredWorkouts[j + 1].WorkoutDate)
                        {
                            // Byt plats
                            var temp = filteredWorkouts[j];
                            filteredWorkouts[j] = filteredWorkouts[j + 1];
                            filteredWorkouts[j + 1] = temp;
                        }
                    }
                }
            }

            // Återställ WorkoutList och lägg till filtrerade träningspass
            WorkoutList.Clear();
            foreach (var workout in filteredWorkouts)
            {
                WorkoutList.Add(workout); // Lägg till filtrerade träningspass i WorkoutList
            }
        }

        // Kommando för att skapa ett nytt träningspass
        private void NewWorkout(object parameter)
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow(); // Skapa nytt fönster för att lägga till träning
            addWorkoutWindow.ShowDialog(); // Visa fönstret som en dialog
            LoadWorkouts(); // Ladda om träningspassen efter att dialogen stängts
        }

        // När ett träningspass klickas på, visa detaljer
        private void WorkoutClicked(Workout workout)
        {
            if (workout != null)
            {
                // Hämta målanvändaren som äger träningspasset
                User targetUser = ManageUser.GetUserByWorkout(workout); // Metod för att hämta användaren som äger träningspasset

                if (targetUser != null)
                {
                    // Skapa detaljerfönstret och skicka den aktuella användaren och målanvändaren
                    WorkoutDetailsWindow detailsWindow = new WorkoutDetailsWindow(workout, ManageUser.currentUser, targetUser);
                    detailsWindow.ShowDialog(); // Visa detaljerna i ett dialogfönster
                }
                else
                {
                    MessageBox.Show("User associated with this workout could not be found.");
                }
            }
        }
    }
}
