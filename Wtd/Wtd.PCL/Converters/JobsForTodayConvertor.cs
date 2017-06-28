using System;
using System.Globalization;
using Wtd.PCL.ViewModels;
using Xamarin.Forms;

namespace Wtd.PCL.Converters
{
    public class JobsForTodayConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((CalendarEntryViewModel)parameter).Date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}