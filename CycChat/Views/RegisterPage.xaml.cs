using CycWpfLibrary;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
      DataContext = DI.loginViewModel;
    }

    private void TextBox_LostFocus(object sender, RoutedEventArgs e)
    {
      TextBoxBehaviors.TextBox_LostFocus(sender, e, DI.loginViewModel, this);
    }
  }
}
