using FitTrack.User;
using FitTrack.Workout;
using System.Windows;
using System.Windows.Controls;

namespace FitTrack.Windows
{
    public partial class AddWorkoutWindow : Window
    {
        public AddWorkoutWindow()
        {
            InitializeComponent();
            WorkoutTypeComboBox.SelectionChanged += WorkoutTypeComboBox_SelectionChanged;
        }

        private void WorkoutTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WorkoutTypeComboBox.SelectedItem != null)
            {
                string selectedWorkoutType = (WorkoutTypeComboBox.SelectedItem as ComboBoxItem).Content.ToString();

                // Toggle visibility based on the selected workout type
                if (selectedWorkoutType == "Cardio")
                {
                    DistanceLabel.Visibility = Visibility.Visible;
                    DistanceTextBox.Visibility = Visibility.Visible;

                    RepetitionsLabel.Visibility = Visibility.Collapsed;
                    RepetitionsTextBox.Visibility = Visibility.Collapsed;
                }
                else if (selectedWorkoutType == "Strength")
                {
                    RepetitionsLabel.Visibility = Visibility.Visible;
                    RepetitionsTextBox.Visibility = Visibility.Visible;

                    DistanceLabel.Visibility = Visibility.Collapsed;
                    DistanceTextBox.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void SaveWorkout_Click(object sender, RoutedEventArgs e)
        {
            
            ComboBoxItem selectedItem = WorkoutTypeComboBox.SelectedItem as ComboBoxItem;
            
            if (selectedItem == null)
            {
                MessageBox.Show("Please select a workout type.");
                return;
            }

            string workoutType = selectedItem.Content.ToString(); 
            string notes = NotesTextBox.Text;

            
            int duration = int.TryParse(DurationTextBox.Text, out int result) ? result : 0;
            int calBurned = 0; 

           
            if (string.IsNullOrEmpty(workoutType) || duration <= 0)
            {
                MessageBox.Show("Please provide valid workout details.");
                return;
            }

            
            Workout.Workout workout;

            if (workoutType == "Cardio")
            {
                
                int distance = int.TryParse(DistanceTextBox.Text, out int distanceResult) ? distanceResult : 0;
                workout = new Workout.CardioWorkout(DateTime.Now, workoutType, TimeSpan.FromMinutes(duration), calBurned, notes, distance);
            }
            else if (workoutType == "Strength")
            {
               
                workout = new StrenghtWorkout(DateTime.Now, workoutType, TimeSpan.FromMinutes(duration), calBurned, notes);
            }
            else
            {
                MessageBox.Show("Invalid workout type.");
                return;
            }

            
            if (FitTrack.Users.User.CurrentUser != null)
            {                
                MessageBox.Show("Workout saved successfully!");
            }
            else
            {
                MessageBox.Show("No user is currently logged in."); 
            }
        }
    }
}
