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
      SimpleIoc.Default.Register<ChatDialogViewModel>();
      SimpleIoc.Default.Register<ChatViewModel>();
    }

    public static MainViewModel MainVM => ServiceLocator.Current.GetInstance<MainViewModel>();
    public static LoginViewModel LoginVM => ServiceLocator.Current.GetInstance<LoginViewModel>();
    public static ChatDialogViewModel ChatDialogVM => ServiceLocator.Current.GetInstance<ChatDialogViewModel>();
    public static ChatViewModel ChatVM => ServiceLocator.Current.GetInstance<ChatViewModel>();
  }
}
