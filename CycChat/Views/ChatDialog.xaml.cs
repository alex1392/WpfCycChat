using CycChat.Core;
using CycWpfLibrary;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CycChat
{
  /// <summary>
  /// ChatDialog.xaml 的互動邏輯
  /// </summary>
  public partial class ChatDialog : Window
  {
    public ChatDialog()
    {
      InitializeComponent();
      DataContext = DI.ChatDialogVM;
      Messenger.Default.Register<Message<string>>(this, DbService<AppDbContext>.errorToken, msg => MessageBox.Show(msg.Content));
    }

    private void FriendName_TextChanged(object sender, TextChangedEventArgs e)
    {
      TextBoxBehaviors.ViewValidation(sender, e, DI.ChatDialogVM, this);
    }
  }
}
