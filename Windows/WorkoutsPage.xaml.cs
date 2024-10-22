using System.Windows.Controls;

namespace FitTrack.Windows
{
    public partial class WorkoutPage : Page
    {
        public WorkoutPage()
        {
            InitializeComponent();
            this.DataContext = new WorkoutPageViewModel();
        }
    }
}
