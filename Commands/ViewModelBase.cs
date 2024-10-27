using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FitTrack.ViewModels
{
    // Bas-klass för ViewModels som implementerar INotifyPropertyChanged för att uppdatera UI vid ändring av egenskaper
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;  // Händelse som meddelar när en egenskap ändras

        // Metod för att utlösa PropertyChanged-händelsen
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Generisk metod för att sätta värde på en egenskap och utlösa PropertyChanged om värdet ändras
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;  // Kontrollera om det nya värdet är samma som det gamla
            storage = value;  // Uppdatera värdet
            OnPropertyChanged(propertyName);  // Utlösa PropertyChanged-händelsen
            return true;
        }
    }
}
