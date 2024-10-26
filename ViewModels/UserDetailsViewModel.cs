using FitTrack.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public class UserDetailsViewModel : INotifyPropertyChanged
{
    private bool _isUsernameVisible;
    private bool _isPasswordVisible;
    private bool _isCountryVisible;
    private bool _isSecurityQuestionVisible;
    private bool _isSecurityAnswerVisible;

    public bool IsUsernameVisible
    {
        get => _isUsernameVisible;
        set { _isUsernameVisible = value; OnPropertyChanged(); }
    }

    public bool IsPasswordVisible
    {
        get => _isPasswordVisible;
        set { _isPasswordVisible = value; OnPropertyChanged(); }
    }

    public bool IsCountryVisible
    {
        get => _isCountryVisible;
        set { _isCountryVisible = value; OnPropertyChanged(); }
    }

    public bool IsSecurityQuestionVisible
    {
        get => _isSecurityQuestionVisible;
        set { _isSecurityQuestionVisible = value; OnPropertyChanged(); }
    }

    public bool IsSecurityAnswerVisible
    {
        get => _isSecurityAnswerVisible;
        set { _isSecurityAnswerVisible = value; OnPropertyChanged(); }
    }

    // Command Properties
    public ICommand ResetPasswordCommand => new RelayCommand(ResetPassword);
    public ICommand ChangeUsernameCommand => new RelayCommand(ChangeUsername);

    // Command Methods
    private void ResetPassword(object parameter) // Accept an object parameter
    {
        IsPasswordVisible = true; 
        IsUsernameVisible = false; 
        IsCountryVisible = false;
        IsSecurityQuestionVisible = false;
        IsSecurityAnswerVisible = false;
    }

    private void ChangeUsername(object parameter) 
    {
        IsUsernameVisible = true; 
        IsPasswordVisible = false; 
        IsCountryVisible = false;
        IsSecurityQuestionVisible = false;
        IsSecurityAnswerVisible = false;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
