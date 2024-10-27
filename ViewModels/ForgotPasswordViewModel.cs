using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using FitTrack.Commands;
using FitTrack.Users;
using FitTrack.Views;

namespace FitTrack.ViewModels
{
    public class ForgotPasswordViewModel : INotifyPropertyChanged
    {
        // Steg i återställningsprocessen
        private int _step = 1;
        private User _user; // Användarobjekt för den aktuella användaren
        private string _username; // Användarnamn
        private string _securityAnswer; // Säkerhetssvar från användaren
        private string _securityQuestion; // Säkerhetsfråga till användaren
        private string _newPassword; // Nytt lösenord som ska sparas
        private string _buttonContent; // Text för knappen baserat på steg

        // Egenskap för användarnamnet, uppdaterar UI vid förändring
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        // Egenskap för säkerhetsfrågan, uppdaterar UI vid förändring
        public string SecurityQuestion
        {
            get => _securityQuestion;
            set
            {
                _securityQuestion = value;
                OnPropertyChanged(nameof(SecurityQuestion));
            }
        }

        // Egenskap för säkerhetssvaret
        public string SecurityAnswer
        {
            get => _securityAnswer;
            set
            {
                _securityAnswer = value;
                OnPropertyChanged(nameof(SecurityAnswer));
            }
        }

        // Egenskap för nytt lösenord
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        // Egenskap för knappens text
        public string ButtonContent
        {
            get => _buttonContent;
            private set
            {
                _buttonContent = value;
                OnPropertyChanged(nameof(ButtonContent));
            }
        }

        // Kommandon för återställning och inloggning
        public ICommand ResetPasswordCommand { get; }
        public ICommand LoginCommand { get; }

        // Konstruktor för att initiera kommandon och uppdatera knapptext
        public ForgotPasswordViewModel()
        {
            ResetPasswordCommand = new RelayCommand(param => ResetPassword());
            LoginCommand = new RelayCommand(param => OpenLoginWindow());
            UpdateButtonContent();
        }

        // Metod för att hantera återställningsprocessen
        private void ResetPassword()
        {
            // Använder switch-sats för att hantera olika steg
            switch (_step)
            {
                case 1:
                    // Kontrollera att användarnamn är ifyllt
                    if (string.IsNullOrEmpty(Username))
                    {
                        MessageBox.Show("Please enter your username.");
                        return;
                    }

                    // Hitta användaren baserat på användarnamn
                    _user = ManageUser.FindUser(Username);

                    // Kontrollera om användaren finns
                    if (_user != null)
                    {
                        // Hämta säkerhetsfråga och visa den för användaren
                        _user.GetSecurityQA(out string answer, out string question);
                        SecurityQuestion = question;
                        Step = 2;
                    }
                    else
                    {
                        // Meddelande om användarnamnet inte hittades
                        MessageBox.Show("Username doesn't exist.");
                    }
                    break;

                case 2:
                    // Kontrollera att säkerhetssvar är ifyllt
                    if (string.IsNullOrEmpty(SecurityAnswer))
                    {
                        MessageBox.Show("Please enter your security answer.");
                        return;
                    }

                    // Kontrollera om säkerhetssvaret är korrekt
                    bool securityCheck = _user.IfSecurityAnswer(SecurityAnswer);
                    if (securityCheck)
                    {
                        Step = 3;
                    }
                    else
                    {
                        MessageBox.Show("Wrong security answer.");
                    }
                    break;

                case 3:
                    // Kontrollera att nytt lösenord är ifyllt
                    if (string.IsNullOrEmpty(NewPassword))
                    {
                        MessageBox.Show("Please enter a new password.");
                        return;
                    }

                    try
                    {
                        // Försök att återställa lösenordet
                        User.ResetPassword(_user.Username, NewPassword);
                        OpenLoginWindow();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    // Öppna inloggningsfönstret efter återställning
                    //OpenLoginWindow();
                    break;

                default:
                    break;
            }
        }

        // Metod för att öppna inloggningsfönstret
        private void OpenLoginWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.Windows[0].Close();
        }

        // Uppdatera knappens innehåll baserat på aktuellt steg
        private void UpdateButtonContent()
        {
            ButtonContent = _step switch
            {
                1 => "Next",
                2 => "Submit Answer",
                3 => "Reset Password",
                _ => "Next"
            };
        }

        // Egenskap för att ändra steg i processen
        public int Step
        {
            get => _step;
            set
            {
                _step = value;
                OnPropertyChanged(nameof(Step));
                UpdateButtonContent();
            }
        }

        // Implementering av INotifyPropertyChanged för att uppdatera UI
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
