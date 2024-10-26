using FitTrack.ViewModels;
using System.Windows;

namespace FitTrack.Windows
{
    public partial class UserDetailsWindow : Window
    {
        public UserDetailsWindow()
        {
            InitializeComponent();
            DataContext = new UserDetailsViewModel();
        }
    }
}
