using System;
using System.Windows.Input;

namespace FitTrack.Commands
{
    // Kommando-klass för att hantera kommandon utan specifik parameter
    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;  // Utför åtgärd
        private readonly Func<object, bool> canExecute;  // Kontrollerar om kommandot kan utföras

        public event EventHandler CanExecuteChanged;  // Händelse som uppdaterar om kommandot kan köras

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        // Metod som returnerar om kommandot kan utföras
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        // Metod som utför kommandot
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        // Metod för att utlösa CanExecuteChanged och uppdatera om kommandot kan köras
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    // Generisk kommando-klass för att hantera kommandon med specifik parameter
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> execute;  // Utför åtgärd med parameter T
        private readonly Func<T, bool> canExecute;  // Kontrollerar om kommandot kan utföras med parameter T

        public event EventHandler CanExecuteChanged;  // Händelse för uppdatering om kommandot kan köras

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        // Metod som returnerar om kommandot kan utföras, baserat på parameter T
        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter is T t ? t : default);
        }

        // Metod som utför kommandot med parameter T
        public void Execute(object parameter)
        {
            if (parameter is T t)
            {
                this.execute(t);
            }
        }

        // Metod för att utlösa CanExecuteChanged och uppdatera om kommandot kan köras
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
