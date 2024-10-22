using FitTrack.Workouts;
using System;
using System.Windows;

namespace FitTrack.Windows
{
    /// <summary>
    /// Interaction logic for WorkoutDetailsWindow.xaml
    /// </summary>
    public partial class WorkoutDetailsWindow : Window
    {
        public Workout CurrentWorkout { get; set; }

        public WorkoutDetailsWindow(Workout clickedItem)
        {
            InitializeComponent();

            CurrentWorkout = clickedItem;
            PopulateWorkoutDetails();
        }

        private void PopulateWorkoutDetails()
        {
            
            WorkoutTypeComboBox.ItemsSource = new string[] { "Cardio", "Strength" };
            WorkoutTypeComboBox.SelectedItem = CurrentWorkout.Type;

            
            DatePicker.SelectedDate = CurrentWorkout.Date;
            DurationTextBox.Text = CurrentWorkout.Duration.ToString(@"hh\:mm");
            CalBurnedTextBox.Text = CurrentWorkout.CalBurned.ToString();
            NotesTextBox.Text = CurrentWorkout.Notes;
        }

        private void SaveWorkout_Click(object sender, RoutedEventArgs e)
        {
            if (WorkoutTypeComboBox.SelectedItem == null || DatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            
            CurrentWorkout.Type = WorkoutTypeComboBox.SelectedItem.ToString();
            CurrentWorkout.Date = DatePicker.SelectedDate ?? DateTime.Now;
            CurrentWorkout.Duration = TimeSpan.Parse(DurationTextBox.Text);
            CurrentWorkout.CalBurned = int.Parse(CalBurnedTextBox.Text);
            CurrentWorkout.Notes = NotesTextBox.Text;

            
            this.Close();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
