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
        private string userInput; // Variabel för att lagra användarens inmatning
        private string expectedAnswer; // Variabel för förväntat svar
        private string questionLabel; // Variabel för att hålla fråga

        // Egenskap för att hålla frågan som ska visas i dialogrutan
        public string QuestionLabel
        {
            get { return questionLabel; }
            set
            {
                questionLabel = value;
                OnPropertyChanged();
            }
        }

        // Egenskap för användarens inmatning
        public string UserInput
        {
            get => userInput;
            set
            {
                userInput = value;
                OnPropertyChanged();
            }
        }

        // Kommando för bekräfta-knappen
        public ICommand ConfirmCommand { get; }
        // Kommando för avbryt-knappen
        public ICommand CancelCommand { get; }

        // Konstruktor för att sätta förväntat svar och fråga
        public InputDialogViewModel(string answer, string question)
        {
            expectedAnswer = answer; // Sätter förväntat svar
            QuestionLabel = question; // Sätter frågan             
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
        }

        // Metod för att hantera bekräftelse när användaren trycker på bekräfta-knappen
        private void Confirm(object parameter)
        {
            // Kontrollera om användarens svar matchar det förväntade svaret
            if (UserInput.Equals(expectedAnswer, StringComparison.OrdinalIgnoreCase))
            {
                // Stänger dialogrutan och indikerar att det var framgångsrikt
                var dialog = Application.Current.Windows.OfType<InputDialog>().FirstOrDefault();
                if (dialog != null)
                {
                    dialog.DialogResult = true; // Ställ in DialogResult till true
                    dialog.Close(); // Stäng dialogrutan
                }
            }
            else
            {
                // Visar ett meddelande om svaret är felaktigt
                MessageBox.Show("Svaret är felaktigt. Försök igen.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Metod för att hantera avbryt när användaren trycker på avbryt-knappen
        private void Cancel(object parameter)
        {
            var dialog = Application.Current.Windows.OfType<InputDialog>().FirstOrDefault();
            if (dialog != null)
            {
                dialog.DialogResult = false; // Ställ in DialogResult till false
                dialog.Close(); // Stäng dialogrutan
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // Metod för att informera UI om att en egenskap har ändrats
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
