using FitTrack.Users;
using FitTrack.ViewModels;
using System.Windows;

namespace FitTrack.Windows
{
    public partial class WorkoutWindow : Window
    {
        public WorkoutWindow(Person user)
        {
            InitializeComponent();
            DataContext = new WorkoutWindowViewModel(user);
            
        }
    }
}
