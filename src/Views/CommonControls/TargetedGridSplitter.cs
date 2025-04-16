using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Automation;

namespace DialogueEditor.Views.CommonControls;

public class TargetedGridSplitter : Thumb, IDisposable
{
	private ColumnDefinition? _col1;
	private ColumnDefinition? _col2;

	private RowDefinition? _row1;
	private RowDefinition? _row2;

	public static readonly DependencyProperty OrientationProperty =
		DependencyProperty.Register
		(
			"Orientation",
			typeof(OrientationType),
			typeof(TargetedGridSplitter),
			new PropertyMetadata(OrientationType.Vertical)
		);

	public static readonly DependencyProperty FirstTargetOrderProperty =
		DependencyProperty.Register
		(
			"FirstTargetOrder",
			typeof(int),
			typeof(TargetedGridSplitter),
			new PropertyMetadata(0)
		);

	public static readonly DependencyProperty SecondTargetOrderProperty =
		DependencyProperty.Register
		(
			"SecondTargetOrder",
			typeof(int),
			typeof(TargetedGridSplitter),
			new PropertyMetadata(1)
		);

	public static readonly DependencyProperty MinTargetSizeProperty =
		DependencyProperty.Register
		(
			"MinTargetSize",
			typeof(int),
			typeof(TargetedGridSplitter),
			new PropertyMetadata(0)
		);

	public static readonly DependencyProperty MaxTargetSizeProperty =
		DependencyProperty.Register
		(
			"MaxTargetSize",
			typeof(int?),
			typeof(TargetedGridSplitter),
			new PropertyMetadata(null)
		);

	public static readonly DependencyProperty EnforceMaxSizeOnTargetProperty =
		DependencyProperty.Register
		(
			"EnforceMaxSizeOnTarget",
			typeof(int?),
			typeof(TargetedGridSplitter),
			new PropertyMetadata(null)
		);

	public OrientationType Orientation
	{
		get => (OrientationType)GetValue(OrientationProperty);
		set => SetValue(OrientationProperty, value);
	}

	public int FirstTargetOrder
	{
		get => (int)GetValue(FirstTargetOrderProperty);
		set => SetValue(FirstTargetOrderProperty, value);
	}

	public int SecondTargetOrder
	{
		get => (int)GetValue(SecondTargetOrderProperty);
		set => SetValue(SecondTargetOrderProperty, value);
	}

	public int MinTargetSize
	{
		get => (int)GetValue(MinTargetSizeProperty);
		set => SetValue(MinTargetSizeProperty, value);
	}

	public int? MaxTargetSize
	{
		get => (int?)GetValue(MaxTargetSizeProperty);
		set => SetValue(MaxTargetSizeProperty, value);
	}

	public int? EnforceMaxSizeOnTarget
	{
		get => (int?)GetValue(EnforceMaxSizeOnTargetProperty);
		set => SetValue(EnforceMaxSizeOnTargetProperty, value);
	}

	public TargetedGridSplitter()
	{
		this.DragStarted += OnDragStarted;
		this.DragDelta += OnDragDelta;
	}

	private void OnDragStarted(object sender, DragStartedEventArgs e)
	{
		if (Parent is Grid grid && IsEnabled)
		{
			if (Orientation == OrientationType.Vertical)
			{
				var cols = grid.ColumnDefinitions;

				if (FirstTargetOrder >= 0 && FirstTargetOrder < cols.Count &&
					SecondTargetOrder >= 0 && SecondTargetOrder < cols.Count)
				{
					_col1 = cols[FirstTargetOrder];
					_col2 = cols[SecondTargetOrder];
				}
			}
			if (Orientation == OrientationType.Horizontal)
			{
				var rows = grid.RowDefinitions;

				if (FirstTargetOrder >= 0 && FirstTargetOrder < rows.Count &&
					SecondTargetOrder >= 0 && SecondTargetOrder < rows.Count)
				{
					_row1 = rows[FirstTargetOrder];
					_row2 = rows[SecondTargetOrder];
				}
			}
		}
	}

	private void OnDragDelta(object sender, DragDeltaEventArgs e)
	{
		if (IsEnabled)
		{
			if (Orientation == OrientationType.Vertical && _col1 is not null && _col2 is not null)
			{
				var result = Drag(e.HorizontalChange, MinTargetSize, _col1.ActualWidth, _col2.ActualWidth);
				_col1.Width = new GridLength(result.Item1, _col1.Width.GridUnitType);
				_col2.Width = new GridLength(result.Item2, _col2.Width.GridUnitType);
			}
			if (Orientation == OrientationType.Horizontal && _row1 is not null && _row2 is not null)
			{
				var result = Drag(e.VerticalChange, MinTargetSize, _row1.ActualHeight, _row2.ActualHeight);
				_row1.Height = new GridLength(result.Item1, _row1.Height.GridUnitType);
				_row2.Height = new GridLength(result.Item2, _row2.Height.GridUnitType);
			}
		}
	}

	private (double, double) Drag
	(
		double delta,
		double minValue,
		double originalValue1,
		double originalValue2
	)
	{
		// Calculate new values
		double newValue1 = originalValue1 + delta;
		double newValue2 = originalValue2 - delta;

		// Enforce minimum values
		if (newValue1 < minValue)
		{
			newValue1 = minValue;
			newValue2 = originalValue1 + originalValue2 - minValue;
		}
		else if (newValue2 < minValue)
		{
			newValue2 = minValue;
			newValue1 = originalValue1 + originalValue2 - minValue;
		}

		// Enforce maximum size on the specified target if configured
		if (MaxTargetSize.HasValue && EnforceMaxSizeOnTarget.HasValue)
		{
			if (EnforceMaxSizeOnTarget == FirstTargetOrder && newValue1 > MaxTargetSize.Value)
			{
				newValue1 = MaxTargetSize.Value;
				newValue2 = originalValue1 + originalValue2 - MaxTargetSize.Value;
			}
			else if (EnforceMaxSizeOnTarget == SecondTargetOrder && newValue2 > MaxTargetSize.Value)
			{
				newValue2 = MaxTargetSize.Value;
				newValue1 = originalValue1 + originalValue2 - MaxTargetSize.Value;
			}
		}

		return (newValue1, newValue2);
	}

	void IDisposable.Dispose()
	{
		this.DragStarted -= OnDragStarted;
		this.DragDelta -= OnDragDelta;
	}
}