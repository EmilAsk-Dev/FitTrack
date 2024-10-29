using FitTrack.Users;
using FitTrack.ViewModels;
using FitTrack.Workouts;
using System.Collections.ObjectModel;
using System.Windows;

namespace FitTrack.Windows
{
    public partial class WorkoutDetailsWindow : Window
    {
        public WorkoutDetailsWindow(Workout workout, User currentUser, User targetUser, ObservableCollection<Workout> _workoutList) 
        {
            InitializeComponent();
            DataContext = new WorkoutDetailsViewModel(workout, this, currentUser, targetUser, _workoutList); 
        }
    }
}
