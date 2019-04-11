using CycChat.Core;
using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
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
    private NewChatDialog chatdialog;
    private Dictionary<string, ChatWindow> chatWindows = new Dictionary<string, ChatWindow>();
    private NewFriendDialog newFriendDialog;

    protected override async void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
      RegisterMessages();

      loginWindow = new LoginWindow();
      loginWindow.Show();

#if DEBUG
      //await DataModel.InitializeAsync(); // make sure data correct at the beginning
      //LoginVM.UserName = "alex1392";
      //LoginVM.Password = "Af04051314!";
      //LoginVM.LoginCommand.Execute(null);
#else
      loginWindow.Show();
#endif


    }

    private void RegisterMessages()
    {
      Messenger.Default.Register<EventMessage>(this, LoginVM.loginToken,
        msg => OnLoggedIn());
      Messenger.Default.Register<EventMessage>(this, MainVM.newChatToken,
        msg => NewChatDialog());
      Messenger.Default.Register<EventMessage<string>>(this, NewChatVM.newChatToken,
        msg =>
        {
          chatdialog.Close();
          OpenChatWindow(msg.Args);
        });
      Messenger.Default.Register<EventMessage<string>>(this, MainVM.openChatToken, msg => OpenChatWindow(msg.Args));
      Messenger.Default.Register<EventMessage>(this, MainVM.newFriendToken, msg => NewFriendDialog());
      Messenger.Default.Register<EventMessage>(this, NewFriendVM.closeToken, msg => CloseNewFriendDialog());

      void CloseNewFriendDialog()
      {
        newFriendDialog.Close();
      }
      void NewFriendDialog()
      {
        newFriendDialog = new NewFriendDialog();
        newFriendDialog.Show();
      }
      void OpenChatWindow(string friendName)
      {
        if (chatWindows.ContainsKey(friendName))
        {
          chatWindows[friendName].Activate();
        }
        else
        {
          var chatWindow = new ChatWindow(friendName);
          chatWindow.Closed += (sender, e) =>
          {
            chatWindows.Remove(friendName);
          };
          chatWindow.Show();
          chatWindows.Add(friendName, chatWindow);
        }
      }
      void NewChatDialog()
      {
        chatdialog = new NewChatDialog();
        chatdialog.Show();
      }
      void OnLoggedIn()
      {
        UserDataModel.Name = LoginVM.UserName;

        mainWindow = new MainWindow();
        loginWindow.Close();
        mainWindow.Show();
      }
    }

  }
}
