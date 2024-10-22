using FitTrack.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitTrack.Windows
{
    /// <summary>
    /// Interaction logic for TwoFASetupPage.xaml
    /// </summary>
    public partial class TwoFASetupPage : Page 
    {
        string username;
        
        public TwoFASetupPage(string username)
        {
            InitializeComponent();
            
            this.username = username;

            SecurityQuestionComboBox.ItemsSource = new string[]
            {
                "What was your childhood nickname?",
                "What is the name of your first pet?",
                "What was the model of your first car?",
                "In what city were you born?",
                "What is your favorite food?"
            };
        }

        private void SecurityQuestionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedQuestion = SecurityQuestionComboBox.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedQuestion))
            {               
                AnswerTextBox.Clear();
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedQuestion = SecurityQuestionComboBox.SelectedItem as string;
            string answer = AnswerTextBox.Text;

            if (string.IsNullOrEmpty(selectedQuestion) || string.IsNullOrEmpty(answer))
            {
                MessageBox.Show("Please select a question and provide an answer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                Person user = Person.FindUser(username);
                if(user != null)
                {
                    user.AddSecAuth(selectedQuestion, answer);
                    MessageBox.Show($"2FA Security Question:\nQuestion: {selectedQuestion}\nAnswer: {answer}", "2FA Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow MainWindow = new MainWindow();
                    MainWindow.Show();
                    Window.GetWindow(this)?.Close();
                }
                
                
                

            }
            catch
            {
                Console.WriteLine("Something went Wrong");
            }
            

            
        }

        private void CreateWithout2FA_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
