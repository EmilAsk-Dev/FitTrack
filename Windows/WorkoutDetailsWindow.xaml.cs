using FitTrack.Users;
using FitTrack.ViewModels;
using FitTrack.Workouts;
using System.Windows;

namespace FitTrack.Windows
{
    public partial class WorkoutDetailsWindow : Window
    {
        public WorkoutDetailsWindow(Workout workout, User currentUser, User targetUser) 
        {
            InitializeComponent();
            DataContext = new WorkoutDetailsViewModel(workout, this, currentUser, targetUser); 
        }
    }
}
