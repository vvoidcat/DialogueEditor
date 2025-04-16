using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace DialogueEditor.Views.Converters;

public class BoolToGridLengthConverter : DependencyObject, IValueConverter
{
	private Dictionary<object, double?> _lastValues = new();

	public static readonly DependencyProperty DefaultSizeProperty =
		DependencyProperty.Register
		(
			"DefaultSize",
			typeof(double),
			typeof(BoolToGridLengthConverter),
			new PropertyMetadata(0.0)
		);

	public double DefaultSize
	{
		get => (double)GetValue(DefaultSizeProperty);
		set => SetValue(DefaultSizeProperty, value);
	}

	public object Convert(object value, Type type, object parameter, CultureInfo culture)
	{
		var key = parameter;
		bool isOpen = (bool)value;

		return isOpen ? new GridLength(_lastValues.GetValueOrDefault(key) ?? DefaultSize) : new GridLength(0);
	}

	public object ConvertBack(object value, Type type, object parameter, CultureInfo culture)
	{
		var key = parameter;

		var height = ((GridLength)value).Value;
		var isVisible = height > 0;

		if (isVisible)
		{
			if (_lastValues.ContainsKey(key))
			{
				_lastValues[key] = height;
			}
			else
			{
				_lastValues.Add(key, height);
			}
		}

		return isVisible;
	}
}
