using CycChat.Core;
using CycWpfLibrary;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CycChat
{
  /// <summary>
  /// ChatDialog.xaml 的互動邏輯
  /// </summary>
  public partial class NewChatDialog : Window
  {
    public NewChatDialog()
    {
      InitializeComponent();
      DataContext = DI.NewChatVM;
      Messenger.Default.Register<Message<string>>(this, DbService<AppDbContext>.errorToken, msg => MessageBox.Show(msg.Content));
    }

    private void SearchBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter)
      {
        DI.NewChatVM.NewChatCommand.Execute(SearchBox.Text);
      }
    }
  }
}
