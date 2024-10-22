using System.Windows;
using FitTrack.ViewModels;

namespace FitTrack.Windows
{
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
        {
            InitializeComponent();
            DataContext = new ForgotPasswordViewModel();
        }
    }
}
