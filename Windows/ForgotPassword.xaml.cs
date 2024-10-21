using System;
using System.Windows;
using FitTrack.Users;

namespace FitTrack.Windows
{
    public partial class ForgotPassword : Window
    {
        private int step = 1;
        private User user = null;

        public ForgotPassword()
        {
            InitializeComponent();

            // Hide elements on startup
            PasswordBox.Visibility = Visibility.Hidden;
            PasswordLabel.Visibility = Visibility.Hidden;
            SecurityAnswerBox.Visibility = Visibility.Hidden;
            SecurityQuestionLabel.Visibility = Visibility.Hidden;

        }

        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {
            switch (step)
            {
                case 1:
                    

                    string username = UsernameTextBox.Text;

                    if (string.IsNullOrEmpty(username))
                    {
                        MessageBox.Show("Please enter your username.");
                        return;
                    }
                    
                    
                    user = Person.FindUser(username) as User;
                    SecurityQuestionLabel.Content = user.SecurityQuestion;
                    if (user != null)
                    {
                        
                        UsernameTextBox.Visibility = Visibility.Collapsed;
                        Usernamelabel.Visibility = Visibility.Collapsed;

                        
                        SecurityAnswerBox.Visibility = Visibility.Visible;
                        SecurityQuestionLabel.Visibility = Visibility.Visible;

                        
                        RegFindButton.Content = "Submit Security Answer";

                        
                        step = 2;
                    }
                    else
                    {
                        MessageBox.Show("Username doesn't exist.");
                    }
                    break;

                case 2:
                    
                    SecurityQuestionLabel.Content = user.SecurityQuestion;
                    string securityAnswer = SecurityAnswerBox.Text;
                    
                    if (string.IsNullOrEmpty(securityAnswer))
                    {
                        MessageBox.Show("Please enter your security answer.");
                        return;
                    }

                    bool securityCheck = user.IfSecurityAnswer(securityAnswer);
                    if (securityCheck)
                    {
                        
                        SecurityAnswerBox.Visibility = Visibility.Collapsed;
                        SecurityQuestionLabel.Visibility = Visibility.Collapsed;

                        
                        PasswordBox.Visibility = Visibility.Visible;
                        PasswordLabel.Visibility = Visibility.Visible;

                        
                        RegFindButton.Content = "Reset Password";

                        
                        step = 3;
                    }
                    else
                    {
                        MessageBox.Show("Wrong security answer.");
                    }
                    break;

                case 3:
                    
                    string newPassword = PasswordBox.Password;

                    if (string.IsNullOrEmpty(newPassword))
                    {
                        MessageBox.Show("Please enter a new password.");
                        return;
                    }

                    
                    User.ResetPassword(user.Username, newPassword);

                    
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                    break;

                default:
                    break;
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
