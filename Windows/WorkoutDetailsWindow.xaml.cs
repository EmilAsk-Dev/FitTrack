using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitTrack.Windows
{
    /// <summary>
    /// Interaction logic for WorkoutDetailsWindow.xaml
    /// </summary>
    public partial class WorkoutDetailsWindow : Window
    {

        public Workout.Workout CurrentWorkout { get; set; }

        public WorkoutDetailsWindow(Workout.Workout clickedItem)
        {
            InitializeComponent();

            CurrentWorkout = clickedItem;
            PopulateWorkoutDetails();

        }

        private void PopulateWorkoutDetails()
        {
            
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
    }
}
