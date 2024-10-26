using System.Windows;
using System.Windows.Controls;
using FitTrack.Commands;
using FitTrack.Users;
using System.Windows.Input;
using FitTrack.Windows;
using FitTrack.Pages;
using FitTrack.Views;

namespace FitTrack.ViewModels
{
    public class WorkoutWindowViewModel : BaseViewModel
    {
        private string _welcomeMessage;
        private ICommand _navigateHomeCommand;
        private ICommand _navigateWorkoutsCommand;
        private ICommand _navigateUserDetails; 
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

        public ICommand NavigateWorkoutsCommand => _navigateWorkoutsCommand ??= new RelayCommand(param => NavigateWorkouts());
        public ICommand NavigateUserDetails => _navigateUserDetails ??= new RelayCommand(param => NavigateUserDetailsPage()); 
        public ICommand LogoutCommand => _logoutCommand ??= new RelayCommand(param => Logout());

        public WorkoutWindowViewModel(Person user)
        {
            WelcomeMessage = "Välkommen " + user.Username;
        }

        private void NavigateWorkouts()
        {
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (window != null)
            {
                
                var frame = window.FindName("ContentFrame") as Frame;
                if (frame != null)
                {
                    frame.Content = new WorkoutPage(); 
                }
            }
        }

        private void NavigateUserDetailsPage() 
        {
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (window != null)
            {
                var frame = window.FindName("ContentFrame") as Frame;
                if (frame != null)
                {
                    frame.Content = new UserDetailsPage(); 
                }
            }
        }

        private void Logout()
        {
            MainWindow mainWindow = new MainWindow();
            ManageUser.currentUser = null;
            mainWindow.Show();
            Application.Current.Windows[0].Close();

        }
    }
}
