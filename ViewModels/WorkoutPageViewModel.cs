using System;
using System.Collections.Generic;
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
        // Observable-kollektion för att hålla listan över träningspass
        public ObservableCollection<Workout> WorkoutList { get; set; }

        private Workout _selectedWorkout;

        // Egenskap för det valda träningspasset
        public Workout SelectedWorkout
        {
            get => _selectedWorkout;
            set
            {
                _selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));

                // Kör kommandot när ett träningspass är valt
                if (_selectedWorkout != null)
                {
                    WorkoutClickCommand.Execute(_selectedWorkout);
                }
            }
        }

        // Kommandon för att skapa nya träningspass och hantera klick på träningspass
        public ICommand NewWorkoutCommand { get; }
        public ICommand WorkoutClickCommand { get; }

        // Konstruktör
        public WorkoutPageViewModel()
        {
            WorkoutList = new ObservableCollection<Workout>();
            LoadWorkouts();

            // Initiera kommandon
            NewWorkoutCommand = new RelayCommand(NewWorkout);
            WorkoutClickCommand = new RelayCommand<Workout>(WorkoutClicked);
        }

        // Metod för att ladda träningspass baserat på användartyp
        private void LoadWorkouts()
        {
            if (ManageUser.currentUser is User user)
            {
                // Rensa och ladda träningspass för vanlig användare
                WorkoutList.Clear();
                foreach (var workout in user.Workouts)
                {
                    Console.WriteLine($"Workout Name: {workout.WorkoutName}");
                    WorkoutList.Add(workout);
                }
            }
            else if (ManageUser.currentUser is AdminUser adminUser)
            {
                // Rensa och ladda alla träningspass för administratörsanvändare
                List<User> allUsers = ManageUser.GetAllUsers();
                var allWorkouts = AdminUser.ManageAllWorkouts(allUsers);

                WorkoutList.Clear();
                foreach (var workout in allWorkouts)
                {
                    WorkoutList.Add(workout);
                }
            }
            else
            {
                // Rensa träningspasslistan för okända användartyper
                WorkoutList.Clear();
            }
        }

        // Metod för att öppna fönstret för att lägga till träningspass
        private void NewWorkout(object parameter)
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.ShowDialog();

            // Ladda om träningspass efter att ha lagt till ett nytt
            LoadWorkouts();
        }

        // Metod för att hantera klick på träningspass
        private void WorkoutClicked(Workout workout)
        {
            if (workout != null)
            {
                // Visa meddelande och öppna detaljerat träningsfönster
                MessageBox.Show($"You clicked on workout: {workout.WorkoutName}");
                WorkoutDetailsWindow workoutDetailsWindow = new WorkoutDetailsWindow(workout);
                workoutDetailsWindow.Show();
            }
            else
            {
                MessageBox.Show("Could not retrieve workout details.");
            }
        }
    }
}
