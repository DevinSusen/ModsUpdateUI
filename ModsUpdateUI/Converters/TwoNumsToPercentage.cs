using System;
using System.Globalization;
using System.Windows.Data;

namespace ModsUpdateUI.Converters
{
    class TwoNumsToPercentage : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int v = System.Convert.ToInt32(values[0]);
            bool isSuc = int.TryParse(values[1].ToString(), out int all);
            if (all == 0)
                return 0;
            return v * 100 / all;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
