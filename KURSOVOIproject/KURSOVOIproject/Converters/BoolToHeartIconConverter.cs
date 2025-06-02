using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace KURSOVOIproject.Converters
{
    public class BoolToHeartIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isFav = (bool)value;
            return isFav ? "heart_filled.png" : "heart_outline.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
