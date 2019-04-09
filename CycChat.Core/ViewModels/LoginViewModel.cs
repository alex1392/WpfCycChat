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
    public readonly object messageToken = new object();
    public readonly object loginToken = new object();

    public LoginViewModel()
    {
      LoginCommand = new RelayCommand(Login, CanLogin);
      RegisterCommand = new RelayCommand(RegisterAsync, CanRegister);
      ToRegisterCommand = new RelayCommand(ToRegisterPage);
      ToLoginCommand = new RelayCommand(ToLoginPage);
      StaticPropertyChanged += LoginViewModel_StaticPropertyChanged;
    }

    private void LoginViewModel_StaticPropertyChanged(object sender, StaticPropertyChangedEventArgs e)
    {
      if (e.ClassName == nameof(DataModel) &&
        e.PropertyName == nameof(DataModel.Users))
      {
        OnPropertyChanged(nameof(UserNames));
      }
    }

    public LoginPages CurrentPage { get; set; } = LoginPages.Login;

    public ICommand LoginCommand { get; set; }
    public ICommand RegisterCommand { get; set; }
    public ICommand ToRegisterCommand { get; set; }
    public ICommand ToLoginCommand { get; set; }

    public List<User> Users => DataModel.Users;
    public List<string> UserNames => Users?.Select(u => u.UserName).ToList();
    public List<string> Passwords => Users?.Select(u => u.Password).ToList();
    public string UserName { get; set; }
    public string Password { get; set; }

    private bool CanRegister()
    {
      return CanLogin();
    }
    private void ToRegisterPage()
    {
      CurrentPage = LoginPages.Register;
    }
    private async void RegisterAsync()
    {
      if (await DataModel.AddAsync<User, AppDbContext>(new User(UserName, Password)))
      {
        Messenger.Default.Send(new Message<string>("Register successfully!"), messageToken);
        ToLoginPage();
      }
    }
    private bool CanLogin()
    {
      return IsViewValid && !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
    }
    private void ToLoginPage()
    {
      CurrentPage = LoginPages.Login;
    }
    private void Login()
    {
      if (Users.Count == 0)
        return;
      else if (!Users.Any(u => u.UserName == UserName))
      {
        Messenger.Default.Send(new Message<string>("No matched user name!"), messageToken);
      }
      else if (!Users.Any(u => u.Password == Password))
      {
        Messenger.Default.Send(new Message<string>("Incorrect password!"), messageToken);
      }
      else
      {
        Messenger.Default.Send(new Message<string>("Log in successfully!"), messageToken);
        Messenger.Default.Send(EventMessage.Empty, loginToken);
      }
    }
  }
}
