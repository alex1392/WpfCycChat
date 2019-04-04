using System;
using System.Windows;

namespace CycChat
{
  /// <summary>
  /// App.xaml 的互動邏輯
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
      DI.loginViewModel.LoggedIn += LoginViewModel_LoggedIn;
      DI.loginWindow.Show();
    }

    private void LoginViewModel_LoggedIn(object sender, EventArgs e)
    {
      DI.loginWindow.Close();
      DI.mainWindow.Show();
    }



  }
}
