using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FitTrack.Converters
{
    public class StepToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int step && parameter is string targetStep && int.TryParse(targetStep, out int target))
            {
                return step == target ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
