using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CycChat.Core
{
  /// <summary>
  /// Provide user related data.
  /// </summary>
  public class UserDataModel : ObservableObject
  {
    private static string username;
    private static List<string> friendNames = new List<string>();
    private static List<ChatRoom> chatRooms = new List<ChatRoom>();
    private static List<Chat> chats = new List<Chat>();

    static UserDataModel()
    {
      StaticPropertyChanged += UserDataModel_StaticPropertyChanged;
    }

    private static void UserDataModel_StaticPropertyChanged(object sender, StaticPropertyChangedEventArgs e)
    {
      if (e.ClassName == nameof(DataModel))
      {
        switch (e.PropertyName)
        {
          case nameof(DataModel.Friends):
            Initialize(nameof(FriendNames));
            break;
          case nameof(DataModel.ChatRooms):
            Initialize(nameof(ChatRooms));
            break;
          case nameof(DataModel.Chats):
            Initialize(nameof(Chats));
            break;
          default:
            break;
        }
      }
    }

    private static void Initialize(string propertyName)
    {
      switch (propertyName)
      {
        case nameof(FriendNames):
          FriendNames = DataModel.Friends
            .Where(f => f.A == username)
            .Select(f => f.B)
            .Concat(DataModel.Friends
                .Where(f => f.B == username)
                .Select(f => f.A))
            .ToList();
          break;
        case nameof(Chats):
          Chats = DataModel.Chats.Where(
            c => c.Sender == username || c.Receiver == username).ToList();
          break;
        case nameof(ChatRooms):
          ChatRooms = DataModel.ChatRooms.Where(
            r => r.Owner == username).ToList();
          break;
        default:
          break;
      }

    }

    public static string Name
    {
      get => username;
      set
      {
        username = value;
        Initialize(nameof(FriendNames));
        Initialize(nameof(Chats));
        Initialize(nameof(ChatRooms));
      }
    }

    public static List<Chat> Chats
    {
      get => chats;
      private set
      {
        if (!value.DataEquals(chats))
        {
          chats = value;
          OnStaticPropertyChanged(nameof(UserDataModel), nameof(Chats));
        }
      }
    }

    public static List<ChatRoom> ChatRooms
    {
      get => chatRooms;
      private set
      {
        if (!value.DataEquals(chatRooms))
        {
          chatRooms = value;
          OnStaticPropertyChanged(nameof(UserDataModel), nameof(ChatRooms));
        }
      }
    }

    public static List<string> FriendNames
    {
      get => friendNames;
      private set
      {
        if (!value.SequenceEqual(friendNames))
        {
          friendNames = value;
          OnStaticPropertyChanged(nameof(UserDataModel), nameof(FriendNames));
        }
      }
    }

  }
}
