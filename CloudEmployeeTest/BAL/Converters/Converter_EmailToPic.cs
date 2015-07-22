/*
 * author:  Jayson Ragasa
 * Date:    July 22, 2015
 */

using CloudEmployee.BAL.Helpers;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace CloudEmployee.BAL.Converters
{
    public class Converter_EmailToPic : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            BitmapImage image = new BitmapImage();
            image.UriSource = new Uri("ms-appx:///Assets/profile_pic_placeholder.png");

            if (value != null)
            {
                if (value.ToString() != string.Empty)
                {
                    string uri = GravatarImage.GetURL(value.ToString(), 128, "g");

                    image.UriSource = new Uri(uri);
                }
            }

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
