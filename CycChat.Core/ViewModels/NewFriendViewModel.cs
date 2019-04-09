using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycChat.Core
{
  public class NewFriendViewModel : ViewModelBase
  {
    public NewFriendViewModel()
    {
      AddFriendCommand = new RelayCommand(AddFriendAsync, CanAddFriend);
    }

    private List<string> UserNames => DataModel.UserNames;
    private List<Friend> AllFriends => DataModel.Friends;
    private string UserName => UserDataModel.Name;
    public RelayCommand AddFriendCommand { get; private set; }
    public string FriendName { get; set; }
    public bool IsFound { get; set; }

    private string _searchName;
    public readonly object messageToken = new object();
    public readonly object closeToken = new object();

    public string SearchName
    {
      get => _searchName;
      set
      {
        _searchName = value;
        Search();
      }
    }

    private bool CanAddFriend()
    {
      return IsFound;
    }
    private async void AddFriendAsync()
    {
      // if they are already friends
      if (AllFriends.Any(f => f.A == UserName && f.B == FriendName
      || f.A == FriendName && f.B == UserName))
      {
        Messenger.Default.Send(new Message<string>("You are already friends."), messageToken);
        return;
      }

      // TODO: send a friend request
      // add friend immediately
      if (await DataModel.AddAsync<Friend, AppDbContext>(new Friend(UserName, FriendName)))
      {
        Messenger.Default.Send(EventMessage.Empty, closeToken);
      }
    }

    private void Search()
    {
      if (UserNames.Contains(_searchName))
      {
        IsFound = true;
        FriendName = _searchName;
      }
      else
      {
        IsFound = false;
        FriendName = "User not found.";
      }
    }
  }
}
