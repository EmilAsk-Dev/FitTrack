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
using FitTrack.Users;

namespace FitTrack.Windows
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
        {
            InitializeComponent();
            PasswordBox.Visibility = Visibility.Hidden;
            PasswordLabel.Visibility = Visibility.Hidden;
            Console.WriteLine("Im in ForgotPerson");
        }

        private void ResetPassword_Click(object sender, RoutedEventArgs e)
        {           
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            bool UserExist = User.IfUserExist(username);
            bool StartingState = true;


            if (UserExist)
            {
                PasswordBox.Visibility = Visibility.Visible;
                PasswordLabel.Visibility = Visibility.Visible;
                StartingState = false;
            }
            else
            {
                MessageBox.Show("Username dosent exist");
            }
            
            if(!StartingState)
            {
                if("Change Password" == RegFindButton.Content)
                {
                    User.ResetPassword(username, password);
                    MainWindow MainWindow = new MainWindow();
                    MainWindow.Show();
                    this.Close();
                }
                else
                {
                    RegFindButton.Content = "Change Password";
                }
                
            }
                    
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }
    }



    

        

        
}
