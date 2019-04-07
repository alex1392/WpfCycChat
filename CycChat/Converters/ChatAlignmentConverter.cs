using CycChat.Core;
using CycWpfLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CycChat
{
  public class ChatAlignmentConverter : ValueConverterBase<ChatAlignmentConverter>
  {
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value.ToStringEx().Equals(UserDataModel.Name))
      {
        return HorizontalAlignment.Right;
      }
      else
      {
        return HorizontalAlignment.Left;
      }
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
