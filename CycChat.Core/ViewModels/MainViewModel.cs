using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CycChat.Core
{
  public class MainViewModel : ViewModelBase
  {
    public MainViewModel()
    {
      ToAccountPageCommand = new RelayCommand(ToAccountPage);
      ToChatListPageCommand = new RelayCommand(ToChatListPage);
      ToFriendPageCommand = new RelayCommand(ToFriendPage);
      NewChatCommand = new RelayCommand(NewChat);
      OpenChatCommand = new RelayCommand<string>(OpenChat);
      NewFriendCommand = new RelayCommand(NewFriend);
      StaticPropertyChanged += MainViewModel_StaticPropertyChanged;
    }

    private void MainViewModel_StaticPropertyChanged(object sender, StaticPropertyChangedEventArgs e)
    {
      if (e.ClassName == nameof(UserDataModel))
      {
        switch (e.PropertyName)
        {
          case nameof(UserDataModel.ChatRooms):
            OnPropertyChanged(nameof(ChatRooms));
            break;
          case nameof(UserDataModel.FriendNames):
            OnPropertyChanged(nameof(FriendNames));
            break;
          default:
            break;
        }
      }
    }

    public MainPages CurrentPage { get; set; } = MainPages.ChatRooms;
    public ObservableCollection<ChatRoom> ChatRooms => new ObservableCollection<ChatRoom>(UserDataModel.ChatRooms);
    public ObservableCollection<string> FriendNames => new ObservableCollection<string>(UserDataModel.FriendNames);

    public ICommand ToAccountPageCommand { get; set; }
    public ICommand ToChatListPageCommand { get; set; }
    public ICommand ToFriendPageCommand { get; private set; }
    public ICommand NewChatCommand { get; set; }
    public ICommand OpenChatCommand { get; private set; }
    public ICommand NewFriendCommand { get; private set; }

    public readonly object newChatToken = new object();
    public readonly object newFriendToken = new object();
    public readonly object openChatToken = new object();

    public void ToAccountPage()
    {
      CurrentPage = MainPages.Account;
    }
    public void ToChatListPage()
    {
      CurrentPage = MainPages.ChatRooms;
    }
    private void ToFriendPage()
    {
      CurrentPage = MainPages.Friends;
    }
    public void NewChat()
    {
      Messenger.Default.Send(EventMessage.Empty, newChatToken);
    }
    private void OpenChat(string friendName)
    {
      Messenger.Default.Send(new EventMessage<string>(friendName), openChatToken);
    }
    private void NewFriend()
    {
      Messenger.Default.Send(EventMessage.Empty, newFriendToken);
    }
  }
}
