﻿using System;
using System.Globalization;
using CycChat.Core;
using CycWpfLibrary;

namespace CycChat
{
  public class MainPageConverter : ValueConverterBase<MainPageConverter>
  {
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      switch ((MainPages)value)
      {
        default:
        case MainPages.Account:
          return new AccountPage();
        case MainPages.ChatList:
          return new ChatRoomsPage();
      }
    }

    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
