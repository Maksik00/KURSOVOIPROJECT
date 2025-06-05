using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace KURSOVOIproject.Converters
{
    /// <summary>
    /// Простой конвертер, который принимает булево значение "IsFavorite"
    /// и возвращает строку с именем соответствующего файла-иконки.
    /// </summary>
    public class BoolToHeartIconConverter : IValueConverter
    {
        // Если значение true → "heart_filled.png", иначе → "heart_outline.png"
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isFav && isFav)
                return "heart_filled.png";
            return "heart_outline.png";
        }

        // Обратное преобразование не требуется
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
