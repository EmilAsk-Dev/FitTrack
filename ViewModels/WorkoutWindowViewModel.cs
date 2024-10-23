using System.Windows;
using System.Windows.Controls;
using FitTrack.Commands;
using FitTrack.Users;
using System.Windows.Input;
using FitTrack.Windows; // Reference to the WorkoutPage

namespace FitTrack.ViewModels
{
    public class WorkoutWindowViewModel : BaseViewModel
    {
        private string _welcomeMessage;
        private ICommand _navigateHomeCommand;
        private ICommand _navigateWorkoutsCommand;
        private ICommand _logoutCommand;

        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged(nameof(WelcomeMessage));
            }
        }

        public ICommand NavigateHomeCommand => _navigateHomeCommand ??= new RelayCommand(param => NavigateHome());
        public ICommand NavigateWorkoutsCommand => _navigateWorkoutsCommand ??= new RelayCommand(param => NavigateWorkouts());
        public ICommand LogoutCommand => _logoutCommand ??= new RelayCommand(param => Logout());

        public WorkoutWindowViewModel(Person user)
        {
            WelcomeMessage = "Välkommen " + user.Username;
        }

        private void NavigateHome()
        {
            // Logic to navigate to HomePage
        }

        private void NavigateWorkouts()
        {
            
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (window != null)
            {
                // Assuming WorkoutWindow.xaml has a Frame named ContentFrame
                var frame = window.FindName("ContentFrame") as Frame;
                if (frame != null)
                {
                    frame.Content = new WorkoutPage(); // Navigate to WorkoutPage
                }
            }
        }

        private void Logout()
        {
            // Logic for logout
        }
    }
}
