using CycWpfLibrary;
using System.Windows;
using System.Windows.Controls;

namespace CycChat
{
  /// <summary>
  /// LoginPage.xaml 的互動邏輯
  /// </summary>
  public partial class RegisterPage : Page
  {
    public RegisterPage()
    {
      InitializeComponent();
      DataContext = DI.LoginVM;
    }

    private void TextBox_LostFocus(object sender, RoutedEventArgs e)
    {
      TextBoxBehaviors.ViewValidation(sender, e, DI.LoginVM, this);
    }
  }
}
