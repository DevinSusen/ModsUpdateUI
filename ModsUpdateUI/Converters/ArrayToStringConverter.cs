using System;
using System.Globalization;
using System.Windows.Data;

namespace ModsUpdateUI.Converters
{
    public class ArrayToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string splitCode = parameter as string;
            if (!(value is string[] vs))
                return "";
            if (!string.IsNullOrEmpty(splitCode))
                return string.Join(splitCode, vs);
            else return string.Join(",", vs);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
