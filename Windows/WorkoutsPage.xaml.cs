using System.Collections.Generic;
using System.Windows.Controls;

namespace FitTrack.Windows
{
    public partial class Workouts : Page
    {
        

        public List<string> WorkoutList { get; set; }


        public Workouts()
        {
            InitializeComponent();

            
            WorkoutList = new List<string>
            {
                "Yoga",
                "Cardio",
                "Strength Training",
                "Pilates",
                "HIIT",
                "Bollar",
                "Maskar"
            };

            

            
            workoutsControl.ItemsSource = WorkoutList;

            
        }
    }
}
