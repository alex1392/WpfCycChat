using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CycChat
{
  /// <summary>
  /// ChatWindow.xaml 的互動邏輯
  /// </summary>
  public partial class ChatWindow : Window
  {
    public ChatWindow(string friendName)
    {
      InitializeComponent();
      DataContext = DI.ChatVM;
    }

    private void ChatTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter && 
        e.KeyboardDevice.Modifiers != ModifierKeys.Shift &&
        DI.ChatVM.SendCommand.CanExecute())
      {
        DI.ChatVM.SendCommand.Execute();
      }
    }
  }
}
