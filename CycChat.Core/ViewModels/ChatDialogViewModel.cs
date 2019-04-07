using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CycChat.Core
{
  public class ChatDialogViewModel : ViewModelBase
  {
    public readonly object newChatToken = new object();

    public List<string> FriendNames => UserDataModel.FriendNames;

    public ChatDialogViewModel()
    {
      NewChatCommand = new RelayCommand(NewChatAsync, CanNewChat);
    }

    private bool CanNewChat() => IsViewValid;
    private async void NewChatAsync()
    {
      if (!UserDataModel.ChatRooms.Any(r => r.FriendName == FriendName))
      {
        // Add new chat room
        var userName = UserDataModel.Name;
        var lastChat = UserDataModel.Chats.LastOrDefault(c => c.Sender == userName || c.Receiver == userName);
        var chatRoom = new ChatRoom
        {
          FriendName = FriendName,
          Owner = userName,
          LastChat = lastChat.Content,
          Time = lastChat.Time,
        };
        if (await UserDataModel.AddAsync<ChatRoom, AppDbContext>(chatRoom))
        {
          Messenger.Default.Send(new EventMessage<string>(FriendName), newChatToken);
        }
      }
      else
      {
        Messenger.Default.Send(new EventMessage<string>(FriendName), newChatToken);
      }
    }

    public string FriendName { get; set; }
    public ICommand NewChatCommand { get; private set; }
  }
}
