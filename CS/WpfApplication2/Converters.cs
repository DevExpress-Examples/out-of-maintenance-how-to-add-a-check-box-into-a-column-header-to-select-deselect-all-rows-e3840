using System;
using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace WpfApplication2
{
	public class CollectionToIsCheckedConverter : IMultiValueConverter
	{
		private ViewModel dataContext;

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			ICollection collection = values[0] as ICollection;
			dataContext = values[1] as ViewModel;

			if ( collection == null || dataContext == null )
				throw new NotSupportedException();

			if ( collection.Count == 0 )
				return false;

			if ( collection.Count == dataContext.List.Count )
				return true;

			return null;
		}

		public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class CollectionToIsThreeStateConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			ICollection collection = values[0] as ICollection;
			ViewModel dataContext = values[1] as ViewModel;

			if ( collection == null || dataContext == null )
				throw new NotSupportedException();

			if ( collection.Count == 0 || collection.Count == dataContext.List.Count )
				return false;

			return true;
		}

		public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}

