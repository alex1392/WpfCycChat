using CycWpfLibrary;
using System.Windows;
using System.Windows.Controls;

namespace CycChat
{
  /// <summary>
  /// LoginPage.xaml 的互動邏輯
  /// </summary>
  public partial class LoginPage : Page
  {
    public LoginPage()
    {
      InitializeComponent();
      DataContext = DI.LoginVM;

      Messenger.Default.Register<Message<string>>(this, DI.LoginVM.messageToken, msg => MessageBox.Show(msg.Content));
    }

    private void TextBox_LostFocus(object sender, RoutedEventArgs e)
    {
      TextBoxBehaviors.ViewValidation(sender, e, DI.LoginVM, this);
    }
  }
}
