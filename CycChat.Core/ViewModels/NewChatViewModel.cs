using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CycChat.Core
{
  public class NewChatViewModel : ViewModelBase
  {
    public NewChatViewModel()
    {
      NewChatCommand = new RelayCommand<string>(NewChatAsync);
      StaticPropertyChanged += NewChatDialogViewModel_StaticPropertyChanged;
      FilterName();
    }

    private void NewChatDialogViewModel_StaticPropertyChanged(object sender, StaticPropertyChangedEventArgs e)
    {
      if (e.ClassName == nameof(UserDataModel))
      {
        if (e.PropertyName == nameof(UserDataModel.FriendNames))
        {
          FilterName();
        }
      }
    }

    public readonly object newChatToken = new object();
    private string _searchName = "";
    private List<string> AllFriendNames => UserDataModel.FriendNames;

    public List<string> FriendNames { get; set; }
    public ICommand NewChatCommand { get; private set; }
    public string SearchName
    {
      get => _searchName;
      set
      {
        _searchName = value;
        FilterName();
      }
    }

    private void FilterName()
    {
      FriendNames = AllFriendNames.Where(n => n.ToLower().Contains(_searchName.ToLower())).ToList();
    }

    private async void NewChatAsync(string friendName)
    {
      // if is not complete friend name
      if (!AllFriendNames.Contains(friendName))
      { // if there is at least one name in the list
        if (FriendNames.Count > 0)
          friendName = FriendNames[0]; // Default: select the first name
        else 
          return;
      }
      // Now friendName must be completed
      // if no existing chat room with this friend
      if (!UserDataModel.ChatRooms.Any(r => r.FriendName == friendName))
      {
        // Add new chat room
        var userName = UserDataModel.Name;
        var lastChat = UserDataModel.Chats.Where(c => c.Sender == userName && c.Receiver == friendName || c.Sender == friendName && c.Receiver == userName).OrderBy(c => c.Time).LastOrDefault() ?? new Chat
        {
          Content = "",
          Time = null,
        };
        var chatRoom = new ChatRoom
        {
          FriendName = friendName,
          Owner = userName,
          LastChat = lastChat.Content,
          Time = lastChat.Time,
        };
        if (await DataModel.AddAsync<ChatRoom, AppDbContext>(chatRoom))
        {
          Messenger.Default.Send(new EventMessage<string>(friendName), newChatToken);
        }
      }
      else
      {
        Messenger.Default.Send(new EventMessage<string>(friendName), newChatToken);
      }
    }

  }
}
