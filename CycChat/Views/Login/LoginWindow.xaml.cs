using CycChat.Core;
using CycWpfLibrary;
using System.Windows;

namespace CycChat
{
  /// <summary>
  /// MainWindow.xaml 的互動邏輯
  /// </summary>
  public partial class LoginWindow : Window
  {
    public LoginWindow()
    {
      InitializeComponent();
      DataContext = DI.LoginVM;
      Messenger.Default.Register<Message<string>>(this, DbService<AppDbContext>.errorToken, msg => MessageBox.Show(msg.Content));
    }
  }
}
