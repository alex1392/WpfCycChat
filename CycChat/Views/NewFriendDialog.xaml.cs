using CycWpfLibrary;
using System.Windows;

namespace CycChat
{
  /// <summary>
  /// NewFriendDialog.xaml 的互動邏輯
  /// </summary>
  public partial class NewFriendDialog : Window
  {
    public NewFriendDialog()
    {
      InitializeComponent();
      DataContext = DI.NewFriendVM;
      Messenger.Default.Register<Message<string>>(this, DI.NewFriendVM.messageToken, msg => MessageBox.Show(msg.Content));
    }
  }
}
