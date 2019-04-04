using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CycChat.Core
{
  public static class DbServices
  {
    private static readonly int msTimeout = 2000;

    /// <summary>
    /// Test that the server is connected
    /// </summary>
    /// <returns>true if the connection is opened</returns>
    private static async Task<bool> CheckConnectionAsync()
    {
      var flag = false;
      string sqlMessage = null;
      try
      {
        var cts = new CancellationTokenSource(msTimeout);
        await NativeMethod.CursorWaitForAsync(() =>
        {
          using (var dbContext = new CycChatDbContext())
          {
            try
            {
              dbContext.Database.Connection.Open();
              flag = true;
            }
            catch (SqlException e)// force timeout, since there is no way to catch exception inside Task.Run
            {
              sqlMessage = e.Message;
              Task.Delay(msTimeout).Wait();
            }
          }
        }, cts.Token);
      }
      catch (TaskCanceledException)
      {
        MessageBox.Show(sqlMessage ?? "Database connection timeout!");
      }
      return flag;
    }

    public static async Task<List<User>> GetUsersAsync()
    {
      List<User> Users = new List<User>();
      if (await CheckConnectionAsync())
      {
        using (var dbContext = new CycChatDbContext())
        {
          Users = dbContext.Users.Select(u => u).ToList();
        }
      }
      return Users;
    }

    public static async Task<bool> AddUserAsync(User user)
    {
      var flag = await CheckConnectionAsync();
      if (flag)
      {
        using (var dbContext = new CycChatDbContext())
        {
          dbContext.Users.Add(user);
          dbContext.SaveChanges();
        }
      }
      return flag;
    }

    public static async Task<List<Chat>> GetUserChatsAsync(string username)
    {
      List<Chat> chats = new List<Chat>();
      if (await CheckConnectionAsync())
      {
        using (var dbContext = new CycChatDbContext())
        {
          chats = dbContext.Chats.Where(c => 
                    c.Sender == username || 
                    c.Receiver == username).ToList();
        }
      }
      return chats;
    }

    public static async Task<List<string>> GetUserFriendsAsync(string username)
    {
      List<string> friendNames = new List<string>();
      if (await CheckConnectionAsync())
      {
        using (var dbContext = new CycChatDbContext())
        {
          var friends = dbContext.Friends;
          foreach (var friend in friends)
          {
            if (friend.A == username)
              friendNames.Add(friend.B);
            else if (friend.B == username)
              friendNames.Add(friend.A);
          }
        }
      }
      return friendNames;
    }
  }
}
