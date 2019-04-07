using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CycChat.Core
{
  /// <summary>
  /// Provide user data from database and the services of munipulating data
  /// </summary>
  public class UserDataModel : DataModelBase<UserDataModel>
  {
    private static string username;
    public static string Name
    {
      get => username;
      set
      {
        username = value;
        InitializeAsync();
      }
    }

    private static async void InitializeAsync()
    {
      var friends = await DbService<AppDbContext>.GetAsync<Friend>();
      var chats = await DbService<AppDbContext>.GetAsync<Chat>();
      var chatRooms = await DbService<AppDbContext>.GetAsync<ChatRoom>();

      FriendNames = friends.Where(f => f.A == username).Select(f => f.B)
        .Concat(friends.Where(f => f.B == username).Select(f => f.A))
        .ToList();
      Chats = chats.Where(c => c.Sender == username || c.Receiver == username).ToList();
      ChatRooms = chatRooms.Where(r => r.Owner == username).ToList();
    }

    public static List<string> FriendNames { get; private set; }

    public static List<Chat> Chats { get; private set; }

    public static List<ChatRoom> ChatRooms { get; private set; }
  }
}
