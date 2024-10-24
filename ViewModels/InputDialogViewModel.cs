using FitTrack.Commands;
using FitTrack.Windows;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModels
{
    public class InputDialogViewModel : INotifyPropertyChanged
    {
        private string userInput;
        private string expectedAnswer; // Property for expected answer
        private string questionLabel; // Rename variable for clarity

        public string QuestionLabel
        {
            get { return questionLabel; }
            set
            {
                questionLabel = value;
                OnPropertyChanged();
            }
        }

        public string UserInput
        {
            get => userInput;
            set
            {
                userInput = value;
                OnPropertyChanged();
            }
        }

        public ICommand ConfirmCommand { get; }
        public ICommand CancelCommand { get; }

        public InputDialogViewModel(string answer, string question, bool twofaCorrect) // Constructor to set the expected answer
        {
            expectedAnswer = answer; // Set expected answer
            QuestionLabel = question; // Set the question label
            twofaCorrect = false;
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
        }

        private void Confirm(object parameter)
        {
            // Check if user input matches the expected answer
            if (UserInput.Equals(expectedAnswer, StringComparison.OrdinalIgnoreCase))
            {
                // Close the dialog and indicate success
                var dialog = Application.Current.Windows.OfType<InputDialog>().FirstOrDefault();
                if (dialog != null)
                {
                    bool twofaCorrect = true;
                    dialog.DialogResult = true; // Set DialogResult to true
                    dialog.Close(); // Close the dialog
                }
            }
            else
            {
                // Show a message box indicating the answer is incorrect
                MessageBox.Show("The answer is incorrect. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel(object parameter)
        {
            var dialog = Application.Current.Windows.OfType<InputDialog>().FirstOrDefault();
            if (dialog != null)
            {
                dialog.DialogResult = false; // Set DialogResult to false
                dialog.Close(); // Close the dialog
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
