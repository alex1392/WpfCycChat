using System;
using System.Windows;

namespace CycChat
{
  /// <summary>
  /// App.xaml 的互動邏輯
  /// </summary>
  public partial class App : Application
  {
    public App()
    {
      DI.loginViewModel.LoggedIn += LoginViewModel_LoggedIn;
    }

    private string userName;
    private string password;

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      DI.loginWindow.Show();
    }

    private void LoginViewModel_LoggedIn(object sender, EventArgs e)
    {
      DI.loginWindow.Close();
      userName = DI.loginViewModel.UserName;
      password = DI.loginViewModel.Password;
      DI.chatWindow.Show();
    }



  }
}
