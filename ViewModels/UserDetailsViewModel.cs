using FitTrack.Commands;
using FitTrack.Users;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModels
{
    public class UserDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; // Event för att meddela om ändringar

        private bool isPasswordChangeVisible; // Variabel för att kontrollera synlighet av lösenordsändring
        public bool IsPasswordChangeVisible
        {
            get => isPasswordChangeVisible;
            set
            {
                isPasswordChangeVisible = value;
                OnPropertyChanged(nameof(IsPasswordChangeVisible)); // Meddela om ändring
            }
        }

        private string username = "DefaultUsername"; // Standardanvändarnamn
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username)); // Meddela om ändring
            }
        }

        private string country = "Sweden"; // Standardland
        public string Country
        {
            get => country;
            set
            {
                country = value;
                OnPropertyChanged(nameof(Country)); // Meddela om ändring
            }
        }

        private string newPassword = ""; // Variabel för nytt lösenord
        public string NewPassword
        {
            get => newPassword;
            set
            {
                newPassword = value;
                OnPropertyChanged(nameof(NewPassword)); // Meddela om ändring
            }
        }

        private string confirmPassword = ""; // Variabel för bekräftelse av nytt lösenord
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword)); // Meddela om ändring
            }
        }

        private string securityQuestion = "What is your favorite color?"; // Standard säkerhetsfråga
        public string SecurityQuestion
        {
            get => securityQuestion;
            set
            {
                securityQuestion = value;
                OnPropertyChanged(nameof(SecurityQuestion)); // Meddela om ändring
                OnPropertyChanged(nameof(IsSecurityQuestionVisible)); // Meddela om ändring av synlighet
            }
        }

        private string securityAnswer = ""; // Variabel för svar på säkerhetsfråga
        public string SecurityAnswer
        {
            get => securityAnswer;
            set
            {
                securityAnswer = value;
                OnPropertyChanged(nameof(SecurityAnswer)); // Meddela om ändring
                OnPropertyChanged(nameof(IsSecurityAnswerVisible)); // Meddela om ändring av synlighet
            }
        }

        public bool IsSecurityQuestionVisible => !string.IsNullOrWhiteSpace(SecurityQuestion); // Kontrollera synlighet för säkerhetsfråga
        public bool IsSecurityAnswerVisible => !string.IsNullOrWhiteSpace(SecurityAnswer); // Kontrollera synlighet för säkerhetssvar

        public ICommand ChangePasswordCommand { get; } // Kommando för att ändra lösenord
        public ICommand SubmitPasswordChangeCommand { get; } // Kommando för att skicka lösenordsändring
        public ICommand SaveChangesCommand { get; } // Kommando för att spara ändringar

        // Konstruktor för att initiera kommandon och ladda användardetaljer
        public UserDetailsViewModel()
        {
            ChangePasswordCommand = new RelayCommand(_ => ChangePassword()); // Initiera kommando för lösenordsändring
            SubmitPasswordChangeCommand = new RelayCommand(_ => SubmitPasswordChange()); // Initiera kommando för att skicka lösenordsändring
            SaveChangesCommand = new RelayCommand(_ => SaveChanges()); // Initiera kommando för att spara ändringar
            LoadUserDetails(); // Ladda användardetaljer
            IsPasswordChangeVisible = false; // Dölja lösenordsändringsfält som standard
        }

        // Metod för att ladda användardetaljer
        private void LoadUserDetails()
        {
            if (ManageUser.currentUser != null)
            {
                User user = ManageUser.currentUser; // Hämta aktuell användare
                Username = user.Username; // Ladda användarnamn
                Country = user.Country; // Ladda land

                user.GetSecurityQA(out string SecurityQuestionTemp, out string SecurityAnswerTemp); // Hämta säkerhetsfråga och svar
                SecurityQuestion = SecurityQuestionTemp; // Sätt säkerhetsfråga
                SecurityAnswer = SecurityAnswerTemp; // Sätt säkerhetssvar

                OnPropertyChanged(nameof(Username)); // Meddela om ändringar i användarnamn
                OnPropertyChanged(nameof(Country)); // Meddela om ändringar i land
            }
        }

        // Metod för att visa lösenordsändringsfält
        private void ChangePassword()
        {
            IsPasswordChangeVisible = true; // Visa fält för lösenordsändring
        }

        // Metod för att skicka lösenordsändring
        private void SubmitPasswordChange()
        {
            if (NewPassword == ConfirmPassword) // Kontrollera att nya lösenordet matchar bekräftelsen
            {
                try
                {
                    User.ResetPassword(ManageUser.currentUser.Username, NewPassword); // Återställ lösenord
                    IsPasswordChangeVisible = false; // Dölja fält för lösenordsändring
                    LoadUserDetails(); // Ladda användardetaljer på nytt
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message); // Visa felmeddelande
                }
            }
            else
            {
                MessageBox.Show("Lösenorden matchar inte."); // Meddela att lösenorden inte matchar
            }
        }

        // Metod för att spara användardetaljer
        private void SaveChanges()
        {
            try
            {
                User user = ManageUser.currentUser; // Hämta aktuell användare

                string newUsername = Username; // Nytt användarnamn
                string newPassword = NewPassword; // Nytt lösenord
                string newCountry = Country; // Nytt land
                string newSecurityQuestion = SecurityQuestion; // Ny säkerhetsfråga
                string newSecurityAnswer = SecurityAnswer; // Nytt säkerhetssvar

                if(newUsername.Length < 4)
                {
                    MessageBox.Show("Must be more than three characters long ");
                    return;
                }

                user.SaveUserDetails(newUsername, newPassword, newCountry, newSecurityQuestion, newSecurityAnswer); // Spara användardetaljer
                MessageBox.Show("Användardetaljer uppdaterades framgångsrikt!"); // Bekräftelsemeddelande
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fel vid uppdatering av användardetaljer: {ex.Message}"); // Visa felmeddelande
            }
        }

        // Metod för att meddela om ändringar i egenskaper
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // Utlös event för ändring
        }
    }
}
