using FitTrack.ViewModels;
using System.Windows.Controls;

namespace FitTrack.Pages
{
    public partial class UserDetailsPage : Page
    {
        public UserDetailsPage()
        {
            InitializeComponent();
            this.DataContext = new UserDetailsViewModel();
        }
    }
}
