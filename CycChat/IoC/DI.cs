using CommonServiceLocator;
using CycChat.Core;
using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CycChat
{
  public static class DI
  {
    static DI()
    {
      ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
      SimpleIoc.Default.Register<LoginViewModel>();
      SimpleIoc.Default.Register<MainViewModel>();
      SimpleIoc.Default.Register<NewChatViewModel>();
      SimpleIoc.Default.Register<ChatViewModel>();
      SimpleIoc.Default.Register<NewFriendViewModel>();
    }

    public static MainViewModel MainVM 
      => ServiceLocator.Current.GetInstance<MainViewModel>();
    public static LoginViewModel LoginVM 
      => ServiceLocator.Current.GetInstance<LoginViewModel>();
    public static NewChatViewModel NewChatVM 
      => ServiceLocator.Current.GetInstance<NewChatViewModel>();
    public static ChatViewModel ChatVM(string friendName)
    {
      var vm = ServiceLocator.Current.GetInstance<ChatViewModel>(friendName);
      vm.FriendName = friendName;
      return vm;
    }
    public static NewFriendViewModel NewFriendVM
      => ServiceLocator.Current.GetInstance<NewFriendViewModel>();
  }
}
