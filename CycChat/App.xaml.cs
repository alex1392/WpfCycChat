using CycChat.Core;
using CycWpfLibrary;
using System;
using System.Linq;
using System.Windows;
using static CycChat.DI;

namespace CycChat
{
  /// <summary>
  /// App.xaml 的互動邏輯
  /// </summary>
  public partial class App : Application
  {
    private LoginWindow loginWindow;
    private MainWindow mainWindow;
    private ChatDialog chatdialog;

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
      RegisterMessages();

      loginWindow = new LoginWindow();
      loginWindow.Show();
    }

    private void RegisterMessages()
    {
      Messenger.Default.Register<Message>(this, LoginVM.loginToken,
        msg => OnLoggedIn());
      Messenger.Default.Register<Message>(this, MainVM.newChatToken,
        msg => OpenChatDialog());
      Messenger.Default.Register<EventMessage<string>>(this, ChatDialogVM.newChatToken,
        msg =>
        {
          chatdialog.Close();
          OpenChatWindow(msg.Args);
        });
    }

    private void OpenChatWindow(string friendName)
    {
      new ChatWindow(ChatDialogVM.FriendName).Show();
    }

    private void OpenChatDialog()
    {
      chatdialog = new ChatDialog();
      chatdialog.Show();
    }

    private void OnLoggedIn()
    {
      UserDataModel.Name = LoginVM.UserName;

      mainWindow = new MainWindow();
      loginWindow.Close();
      mainWindow.Show();
    }
  }
}
