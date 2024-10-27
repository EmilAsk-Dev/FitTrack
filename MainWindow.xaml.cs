using System.Windows;
using System.Windows.Controls;
using FitTrack.ViewModels;

namespace FitTrack.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        //Det ändra som bryter mot mvvm modelen ( tar imot passwod från ui och skickar vidare till viewmodel)
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.Password = passwordBox.Password; 
            }
        }
    }
}
