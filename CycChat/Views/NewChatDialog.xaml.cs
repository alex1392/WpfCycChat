using CycChat.Core;
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
  /// NewChatDialog.xaml 的互動邏輯
  /// </summary>
  public partial class NewChatDialog : Window
  {
    public NewChatDialog()
    {
      InitializeComponent();
    }

    public string FriendName { get; set; }

    private async void ChatButton_ClickAsync(object sender, RoutedEventArgs e)
    {
      var friendNames = await DbServices.GetUserFriendsAsync(Data.UserName);
      if (!friendNames.Any(f => f == FriendNameTextBox.Text))
      {
        MessageBox.Show("No friend matches the user name.");
      }
      else
      {
        FriendName = FriendNameTextBox.Text;
        // Add new chat to chat list
        

        DialogResult = true;
        Close();

      }
    }
  }
}
