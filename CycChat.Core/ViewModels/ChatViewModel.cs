using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycChat.Core
{
  public class ChatViewModel : ViewModelBase
  {
    public ChatViewModel()
    {
      SendCommand = new RelayCommand(SendAsync, CanSend);
      StaticPropertyChanged += ChatViewModel_StaticPropertyChanged;
    }

    private void ChatViewModel_StaticPropertyChanged(object sender, StaticPropertyChangedEventArgs e)
    {
      if (e.ClassName == nameof(UserDataModel))
      {
        switch (e.PropertyName)
        {
          case nameof(UserDataModel.Chats):
            OnPropertyChanged(nameof(FriendChats));
            break;
          default:
            break;
        }
      }
    }

    public RelayCommand SendCommand { get; private set; }
    public string ChatText { get; set; }

    private string UserName => UserDataModel.Name;
    private List<Chat> AllChats => UserDataModel.Chats;
    public List<Chat> FriendChats =>
      AllChats.Where(c => c.Sender == UserName && c.Receiver == FriendName)
      .Concat(
      AllChats.Where(c => c.Sender == FriendName && c.Receiver == UserName))
      .OrderBy(c => c.Time)
      .ToList();

    public string FriendName { get; set; }

    private async void SendAsync()
    {
      var currentChat = new Chat
      {
        Sender = UserName,
        Receiver = FriendName,
        Content = ChatText,
        Time = DateTime.Now,
      };
      var newUserChatRoom = new ChatRoom
      {
        Owner = UserName,
        FriendName = FriendName,
        LastChat = currentChat.Content,
        Time = currentChat.Time,
      };
      var newFriendChatRoom = new ChatRoom
      {
        Owner = FriendName,
        FriendName = UserName,
        LastChat = currentChat.Content,
        Time = currentChat.Time,
      };
      bool findUserChatRoom(ChatRoom r)
        => r.Owner == UserName && r.FriendName == FriendName;
      bool findFriendChatRoom(ChatRoom r)
        => r.Owner == FriendName && r.FriendName == UserName;
      if (await DataModel.AddAsync<Chat, AppDbContext>(currentChat))
      {
        // update user's chatRoom
        await DataModel.SetAsync<ChatRoom, AppDbContext>(findUserChatRoom, newUserChatRoom);
        // update friend's chatRoom, if exist
        if (DataModel.ChatRooms.Any(findFriendChatRoom))
        {
          await DataModel.SetAsync<ChatRoom, AppDbContext>(findFriendChatRoom, newFriendChatRoom);
        }
        // clear chat text
        ChatText = null;
      }
    }

    private bool CanSend() => !string.IsNullOrEmpty(ChatText);
  }
}
