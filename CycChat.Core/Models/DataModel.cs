using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace CycChat.Core
{
  /// <summary>
  /// Provide data directly extracted from database.
  /// </summary>
  public class DataModel : DataModelBase<DataModel>
  {
    static DataModel()
    {
      var timer = new DispatcherTimer(
        TimeSpan.FromSeconds(1),
        DispatcherPriority.Background,
        async (sender, e) => await InitializeAsync(),
        Application.Current.Dispatcher);
      timer.Start();
    }

    public static async Task InitializeAsync()
    {
      Users = await DbService<AppDbContext>.GetAsync<User>();
      Friends = await DbService<AppDbContext>.GetAsync<Friend>();
      Chats = await DbService<AppDbContext>.GetAsync<Chat>();
      ChatRooms = await DbService<AppDbContext>.GetAsync<ChatRoom>();
    }

    private static List<User> users = new List<User>();
    private static List<Friend> friends = new List<Friend>();
    private static List<ChatRoom> chatRooms = new List<ChatRoom>();
    private static List<Chat> chats = new List<Chat>();

    public static List<User> Users
    {
      get => users;
      set
      {
        if (!value.DataEquals(users))
        {
          users = value;
          OnStaticPropertyChanged(nameof(DataModel), nameof(Users));
        }
      }
    }

    public static List<Friend> Friends
    {
      get => friends;
      set
      {
        if (!value.DataEquals(friends))
        {
          friends = value;
          OnStaticPropertyChanged(nameof(DataModel), nameof(Friends));
        }
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
          OnStaticPropertyChanged(nameof(DataModel), nameof(Chats));
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
          OnStaticPropertyChanged(nameof(DataModel), nameof(ChatRooms));
        }
      }
    }

    public static List<string> UserNames => users.Select(u => u.UserName).ToList();
  }
}
