using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycChat.Core
{
  public class ChatViewModel : ViewModelBase
  {
    public RelayCommand SendCommand { get; private set; }
    public string Chat { get; set; }
    public List<Chat> Chats { get; set; }

    public ChatViewModel()
    {
      SendCommand = new RelayCommand(Send, CanSend);
    }

    private void Send()
    {
      // add new chat to userdata.allchats

    }

    private bool CanSend() => !string.IsNullOrEmpty(Chat);
  }
}
