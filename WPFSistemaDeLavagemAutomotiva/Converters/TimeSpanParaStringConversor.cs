using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFSistemaDeLavagemAutomotiva.Converters
{
    // Classe para converter de timespan para string
    public class TimeSpanParaStringConversor : IValueConverter
    {
        // Converte TimeSpan -> string (para exibição no TextBox)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan ts)
            {
                return ts.ToString(@"hh\:mm"); // Formato "HH:mm"
            }
            return "";
        }

        // Converte string -> TimeSpan (quando o usuário digita no TextBox)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (TimeSpan.TryParse(value?.ToString(), out TimeSpan ts))
            {
                return ts;
            }
            return TimeSpan.Zero;
        }
    }
}
