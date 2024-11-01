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
        private string _welcomeMessage; // Välkomstmeddelande
        private ICommand _navigateHomeCommand; // Kommandon för navigation
        private ICommand _navigateWorkoutsCommand; // Kommando för att navigera till träningspass
        private ICommand _navigateUserDetails; // Kommando för att navigera till användardetaljer
        private ICommand _logoutCommand; // Kommando för att logga ut
        private ICommand _openAddWorkoutWindowCommand;
        private ICommand _showInfoCommand;

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
        public ICommand OpenAddWorkoutWindowCommand => _openAddWorkoutWindowCommand ??= new RelayCommand(param => ExecuteOpenAddWorkoutWindowCommand());
        public ICommand ShowInfoCommand => _showInfoCommand ??= new RelayCommand(param => ShowInfo());

        // Konstruktör
        public WorkoutWindowViewModel(Person user)
        {
            WelcomeMessage = "Välkommen " + user.Username; // Sätter välkomstmeddelande
        }

        private void ExecuteOpenAddWorkoutWindowCommand()
        {
            var addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.Show();
            
        }

        // Metod för att navigera till träningspass
        private void NavigateWorkouts()
        {
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (window != null)
            {
                var frame = window.FindName("ContentFrame") as Frame; // Hitta ContentFrame
                if (frame != null)
                {
                    frame.Content = new WorkoutPage(); // Sätt innehållet till WorkoutPage
                }
            }
        }

        // Metod för att navigera till användardetaljer
        private void NavigateUserDetailsPage()
        {
            Window window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (window != null)
            {
                var frame = window.FindName("ContentFrame") as Frame; // Hitta ContentFrame
                if (frame != null)
                {
                    frame.Content = new UserDetailsPage(); // Sätt innehållet till UserDetailsPage
                }
            }
        }

        // Metod för att logga ut
        private void Logout()
        {
            MainWindow mainWindow = new MainWindow(); // Skapa nytt huvudfönster
            ManageUser.currentUser = null; // Återställ aktuell användare
            mainWindow.Show(); // Visa huvudfönstret
            Application.Current.Windows[0].Close(); // Stäng det aktiva fönstret
        }
        
        private void ShowInfo()
        {
            MessageBox.Show("Vällkommen till denna appen. I workouts har du alla dina workouts där du även kan lägga till och ta bort dom genom att clicka på workoutsen. Du har även en user button där du kan redigera din användare" +
                "", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
