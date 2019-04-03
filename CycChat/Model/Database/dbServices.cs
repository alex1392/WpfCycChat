using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CycChat
{
  public static class dbServices
  {
    private static readonly int msTimeout = 2000;

    public static async Task<bool> dbConnectAsync(Action action)
    {
      try
      {
        var cts = new CancellationTokenSource(msTimeout);
        await NativeMethod.CursorWaitForAsync(() =>
        {
          try
          {
            action.Invoke();
          }
          catch (SqlException) // force timeout, since there is no way to catch exception inside Task.Run
          {
            Task.Delay(msTimeout).Wait();
          }
        }, cts.Token);
        return true;
      }
      catch (TaskCanceledException)
      {
        MessageBox.Show("Database connection timeout!");
        return false;
      }
    }

    public static async Task<List<User>> GetUsersAsync()
    {
      List<User> Users = new List<User>();
      await dbConnectAsync(() =>
      {
        try
        {
          using (var dbContext = new DataBaseModel())
          {
            Users = dbContext.Users.Select(u => u).ToList();
          }
        }
        catch (Exception)
        {
          throw;
        }
      });
      return Users;
    }

    public static async Task<bool> AddUserAsync(User user)
    {
      return await dbConnectAsync(() =>
      {
        try
        {
          using (var dbContext = new DataBaseModel())
          {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
          }
        }
        catch (Exception)
        {
          throw;
        }
      });
    }
  }
}
