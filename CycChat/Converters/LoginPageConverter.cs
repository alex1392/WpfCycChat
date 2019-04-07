using CycChat.Core;
using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycChat
{
  public class LoginPageConverter : ValueConverterBase<LoginPageConverter>
  {
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      switch ((LoginPages)value)
      {
        default:
        case LoginPages.Login:
          return new LoginPage();
        case LoginPages.Register:
          return new RegisterPage();
      }
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
