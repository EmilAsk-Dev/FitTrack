using FitTrack.Commands;
using FitTrack.Users;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace FitTrack.ViewModels
{
    public class UserDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isPasswordChangeVisible;
        public bool IsPasswordChangeVisible
        {
            get => isPasswordChangeVisible;
            set
            {
                isPasswordChangeVisible = value;
                OnPropertyChanged(nameof(IsPasswordChangeVisible));
            }
        }

        private string username = "DefaultUsername"; 
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string country = "Sweden"; 
        public string Country
        {
            get => country;
            set
            {
                country = value; 
                OnPropertyChanged(nameof(Country));
            }
        }

        private string newPassword = ""; 
        public string NewPassword
        {
            get => newPassword;
            set
            {
                newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        private string confirmPassword = ""; 
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        
        private string securityQuestion = "What is your favorite color?"; 
        public string SecurityQuestion
        {
            get => securityQuestion;
            set
            {
                securityQuestion = value;
                OnPropertyChanged(nameof(SecurityQuestion));
                OnPropertyChanged(nameof(IsSecurityQuestionVisible));
            }
        }

        private string securityAnswer = ""; 
        public string SecurityAnswer
        {
            get => securityAnswer;
            set
            {
                securityAnswer = value;
                OnPropertyChanged(nameof(SecurityAnswer));
                OnPropertyChanged(nameof(IsSecurityAnswerVisible));
            }
        }

        
        public bool IsSecurityQuestionVisible => !string.IsNullOrWhiteSpace(SecurityQuestion);
        public bool IsSecurityAnswerVisible => !string.IsNullOrWhiteSpace(SecurityAnswer);

        public ICommand ChangePasswordCommand { get; }
        public ICommand SubmitPasswordChangeCommand { get; }
        public ICommand SaveChangesCommand { get; }

        public UserDetailsViewModel()
        {
            ChangePasswordCommand = new RelayCommand(_ => ChangePassword());
            SubmitPasswordChangeCommand = new RelayCommand(_ => SubmitPasswordChange());
            SaveChangesCommand = new RelayCommand(_ => SaveChanges());
            LoadUserDetails();
            IsPasswordChangeVisible = false;
        }

        private void LoadUserDetails()
        {
            if (ManageUser.currentUser != null)
            {
                User user = ManageUser.currentUser;
                Username = user.Username;

                Country = user.Country;

                user.GetSecurityQA(out string SecurityQuestionTemp, out string SecurityAnswerTemp);
                SecurityQuestion = SecurityQuestionTemp;
                SecurityAnswer = SecurityAnswerTemp;

                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(Country));
            }
        }

        private void ChangePassword()
        {
            IsPasswordChangeVisible = true;
        }

        private void SubmitPasswordChange()
        {
            if (NewPassword == ConfirmPassword)
            {
                try
                {
                    User.ResetPassword(ManageUser.currentUser.Username, NewPassword);
                    IsPasswordChangeVisible = false;
                    LoadUserDetails();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Passwords do not match.");
            }
        }

        private void SaveChanges()
        {
            try
            {
                User user = ManageUser.currentUser;

                string newUsername = Username;
                string newPassword = NewPassword;
                string newCountry = Country;
                string newSecurityQuestion = SecurityQuestion;
                string newSecurityAnswer = SecurityAnswer;

                user.SaveUserDetails(newUsername, newPassword, newCountry, newSecurityQuestion, newSecurityAnswer);
                MessageBox.Show("User details updated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user details: {ex.Message}");
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
