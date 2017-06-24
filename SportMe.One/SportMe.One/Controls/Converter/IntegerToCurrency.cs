using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SportMe.One.Controls.Converter
{
    public class IntegerToCurrency : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (string.IsNullOrEmpty(value.ToString()))
            {
                return value;
            }

            decimal currency = (int)value / 100m;
            return currency.ToString("c", culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
