using FitTrack.ViewModels;
using System.Windows;

namespace FitTrack.Windows
{
    public partial class AddWorkoutWindow : Window
    {
        public AddWorkoutWindow()
        {
            InitializeComponent();
            DataContext = new AddWorkoutViewModel();
        }
    }
}
