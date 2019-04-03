using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CycChat.Core
{
  public class LoginViewModel : ViewModelBase
  {
    public LoginViewModel()
    {
      LoginCommand = new RelayCommand(LoginAsync, CanLogin);
      RegisterCommand = new RelayCommand(RegisterAsync, CanRegister);
      ToRegisterCommand = new RelayCommand(ToRegisterPage);
      ToLoginCommand = new RelayCommand(ToLoginPage);
      PropertyChanged += ViewModel_PropertyChangedAsync;
      CurrentPage = AppPages.Login;
    }

    private async void ViewModel_PropertyChangedAsync(object sender, PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case nameof(CurrentPage):
          Users = await DbServices.GetUsersAsync();
          break;
        default:
          break;
      }
    }

    public AppPages CurrentPage { get; set; } = AppPages.None;

    public ICommand LoginCommand { get; set; }
    public ICommand RegisterCommand { get; set; }
    public ICommand ToRegisterCommand { get; set; }
    public ICommand ToLoginCommand { get; set; }

    public List<User> Users { get; set; }
    public List<string> UserNames => Users?.Select(u => u.UserName).ToList();
    public List<string> Passwords => Users?.Select(u => u.Password).ToList();
    public string UserName { get; set; }
    public string Password { get; set; }

    public event EventHandler LoggedIn;
    protected virtual void OnLoggedIn()
    {
      LoggedIn?.Invoke(this, null);
    }

    private async void LoginAsync()
    {
      Users = await DbServices.GetUsersAsync();
      if (Users.Count == 0)
        return;
      else if (!Users.Any(u => u.UserName == UserName))
      {
        MessageBox.Show("No matched user name!");
      }
      else if (!Users.Any(u => u.Password == Password))
      {
        MessageBox.Show("Incorrect password!");
      }
      else
      {
        MessageBox.Show("Log in successfully!");
        OnLoggedIn();
      }
    }
    private async void RegisterAsync()
    {
      if (await DbServices.AddUserAsync(new User(UserName, Password)))
      {
        MessageBox.Show("Register successfully!");
        ToLoginPage();
      }
    }
    private bool CanLogin()
    {
      return IsViewValid && !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
    }
    private bool CanRegister()
    {
      return CanLogin();
    }
    private void ToLoginPage()
    {
      CurrentPage = AppPages.Login;
    }
    private void ToRegisterPage()
    {
      CurrentPage = AppPages.Register;
    }
  }
}
