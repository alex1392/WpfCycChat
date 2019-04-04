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
  public class LastChat
  {
    public LastChat(string friendName, string content)
    {
      FriendName = friendName;
      Content = content;
    }
    public string FriendName { get; set; }
    public string Content { get; set; }
  }

  public class MainViewModel : ViewModelBase
  {
    public MainViewModel()
    {
      ToAccountPageCommand = new RelayCommand(ToAccountPage);
      ToChatListPageCommand = new RelayCommand(ToChatListPage);
    }

    public async void Initialize()
    {
      userName = Data.UserName;
      friendNames = await DbServices.GetUserFriendsAsync(userName);
      userChats = await DbServices.GetUserChatsAsync(userName);
      foreach (var name in friendNames)
      {
        friendChatLists[name] = userChats
          .Where(c => c.Sender == name || c.Receiver == name)
          .OrderByDescending(c => c.Time).ToList();
      }
      LastChats = friendChatLists
        .Where(L => L.Value.Count > 0)
        .Select(L => new LastChat(L.Key, L.Value[0].Content)).ToList();
    }

    private string userName;
    private List<string> friendNames;
    private List<Chat> userChats;
    private Dictionary<string, List<Chat>> friendChatLists = new Dictionary<string, List<Chat>>();

    public MainPages CurrentPage { get; set; } = MainPages.ChatList;

    public List<LastChat> LastChats { get; set; }

    public ICommand ToAccountPageCommand { get; set; }
    public ICommand ToChatListPageCommand { get; set; }

    public void ToAccountPage()
    {
      CurrentPage = MainPages.Account;
    }
    public void ToChatListPage()
    {
      CurrentPage = MainPages.ChatList;
    }

  }
}
