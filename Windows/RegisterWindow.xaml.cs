using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using FitTrack.Users;
using FitTrack.ViewModels;

namespace FitTrack.Windows
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();                        
            DataContext = new RegisterViewModel();
        }       
    }
}
