using System.Windows;
using FitTrack.ViewModels;

namespace FitTrack.Windows
{
    public partial class RegisterWindow : Window
    {
        private RegisterViewModel _viewModel;

        public RegisterWindow()
        {
            InitializeComponent();
            _viewModel = new RegisterViewModel(this);
            this.DataContext = _viewModel;
        }

        // Handle PasswordBox password change manually
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.Password = PasswordBox.Password;
            }
        }

        // Handle ConfirmPasswordBox password change manually
        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.ConfirmPassword = ConfirmPasswordBox.Password;
            }
        }

        // Register button click event
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Call RegisterCommand from ViewModel or handle the logic here
            _viewModel.RegisterCommand.Execute(null);
        }
    }
}
