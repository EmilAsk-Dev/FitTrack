﻿using FitTrack.ViewModels;
using System.Windows;

namespace FitTrack.Windows
{
    /// <summary>
    /// Interaction logic for InputDialog.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        public InputDialog(string answer, string question, bool twofaCorrect)
        {
            InitializeComponent();
            DataContext = new InputDialogViewModel(answer, question, twofaCorrect);
        }
    }
}
