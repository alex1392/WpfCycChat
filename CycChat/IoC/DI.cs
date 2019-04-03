using CycChat.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycChat
{
  public static class DI
  {
    public static readonly LoginViewModel loginViewModel = new LoginViewModel();
    public static readonly LoginWindow loginWindow = new LoginWindow();
    public static readonly LoginPage loginPage = new LoginPage();
    public static readonly RegisterPage registerPage = new RegisterPage();

    public static readonly ChatWindow chatWindow = new ChatWindow();
  }
}
