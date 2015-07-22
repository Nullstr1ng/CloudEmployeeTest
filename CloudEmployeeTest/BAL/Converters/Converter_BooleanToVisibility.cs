/*
 * author:  Jayson Ragasa
 * Date:    July 22, 2015
 */

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace CloudEmployee.BAL.Converters
{
    public class Converter_BooleanToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
