using CycWpfLibrary;
using System;
using System.Collections.Generic;
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
      NewChatCommand = new RelayCommand(NewChat);
      InitializeAsync();
    }

    public async void InitializeAsync()
    {
      //foreach (var name in friendNames)
      //{
      //  friendChatLists[name] = userChats
      //    .Where(c => c.Sender == name || c.Receiver == name)
      //    .OrderByDescending(c => c.Time).ToList();
      //}
    }

    private string UserName => UserDataModel.Name;
    private List<string> FriendNames => UserDataModel.FriendNames;
    private List<Chat> UserAllChats => UserDataModel.Chats;
    private List<ChatRoom> UserChatRooms => UserDataModel.ChatRooms;

    //private Dictionary<string, List<Chat>> friendChatLists = new Dictionary<string, List<Chat>>();

    public MainPages CurrentPage { get; set; } = MainPages.ChatList;

    public List<ChatRoom> ChatRooms { get; set; }

    public ICommand ToAccountPageCommand { get; set; }
    public ICommand ToChatListPageCommand { get; set; }
    public ICommand NewChatCommand { get; set; }
    public readonly object newChatToken = new object();

    public void ToAccountPage()
    {
      CurrentPage = MainPages.Account;
    }
    public void ToChatListPage()
    {
      CurrentPage = MainPages.ChatList;
    }
    public void NewChat()
    {
      Messenger.Default.Send(new Message(), newChatToken);
    }

  }
}
