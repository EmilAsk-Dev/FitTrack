using FitTrack.Workouts;
using System.Windows;

namespace FitTrack.Windows
{
    public partial class WorkoutDetailsWindow : Window
    {
        public WorkoutDetailsWindow(Workout workout)
        {
            InitializeComponent();            

            DataContext = new WorkoutDetailsViewModel(workout, this);

        }
    }
}
