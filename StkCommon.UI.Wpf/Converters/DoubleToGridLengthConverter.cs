using System;
using System.Windows;

namespace StkCommon.UI.Wpf.Converters
{
	/// <summary>
	/// ����������� double � GridLength, GridUnitType ������� ����� ��������, �� ��������� GridUnitType.Pixel
	/// </summary>
	public class DoubleToGridLengthConverter : ValueConverterBase
	{
		public DoubleToGridLengthConverter()
		{
			UnitType = GridUnitType.Pixel;
		}

		public GridUnitType UnitType { get; set; }

		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (!(value is double))
				return value;
			var valueDouble = (double)value;
			return new GridLength(valueDouble, UnitType);
		}

		public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (!(value is GridLength))
				return value;
			var valueLength = (GridLength)value;
			return valueLength.Value;
		}
	}
}