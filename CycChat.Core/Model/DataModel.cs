using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycChat.Core
{
  public class DataModel : DataModelBase<DataModel>
  {
    static DataModel()
    {
      InitializeAsync();
    }

    public static async void InitializeAsync()
    {
      Users = await DbService<AppDbContext>.GetAsync<User>();
    }

    private static List<User> users;
    public static List<User> Users
    {
      get => users;
      set
      {
        users = value;
        OnStaticPropertyChanged(nameof(DataModel), nameof(Users));
      }
    }

  }
}
