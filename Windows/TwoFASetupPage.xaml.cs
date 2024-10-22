using FitTrack.Users;
using FitTrack.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FitTrack.Windows
{
    
    public partial class TwoFASetupPage : Page
    {
        public TwoFASetupPage(string username)
        {
            InitializeComponent();
            DataContext = new TwoFASetupViewModel(username); 
        }
    }
}
