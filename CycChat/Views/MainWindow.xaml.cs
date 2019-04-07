using CycChat.Core;
using CycWpfLibrary;
using System.Windows;

namespace CycChat
{
  /// <summary>
  /// MainWindow.xaml 的互動邏輯
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      DataContext = DI.MainVM;
      Messenger.Default.Register<Message<string>>(this, DbService<AppDbContext>.errorToken, msg => MessageBox.Show(msg.Content));
    }
  }
}
