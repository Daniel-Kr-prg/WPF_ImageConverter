using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Converter.Converters
{
    class MemSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long memsize && memsize > 0)
            {
                return FormatMemorySize(memsize);
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private string FormatMemorySize(long sizeInBytes)
        {
            const long kilobyte = 1024;
            const long megabyte = kilobyte * 1024;

            if (sizeInBytes < kilobyte)
            {
                return $"{sizeInBytes} B";
            }
            else if (sizeInBytes < megabyte)
            {
                return $"{(double)sizeInBytes / kilobyte:F2} KB";
            }
            else
            {
                return $"{(double)sizeInBytes / megabyte:F2} MB";
            }
        }
    }
}
