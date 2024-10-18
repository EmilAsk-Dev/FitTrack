using System.Windows;

namespace FitTrack.Windows
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void AlreadyAcc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
            
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
