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
  public class PageConverter : ValueConverterBase<PageConverter>
  {
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      switch ((AppPages)value)
      {
        default:
        case AppPages.Login:
          return DI.loginPage;
        case AppPages.Register:
          return DI.registerPage;
      }
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
