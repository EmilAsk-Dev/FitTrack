using FitTrack.Commands;
using FitTrack.Users;
using System.Windows.Input;

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
            // For example, you might raise an event or use a navigation service
        }

        private void NavigateWorkouts()
        {
            // Logic to navigate to Workouts
        }

        private void Logout()
        {
            // Logic for logout
        }
    }
}
