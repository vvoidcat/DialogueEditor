using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Input;

namespace DialogueEditor.Views.CommonControls;

/// <summary>
/// Interaction logic for ShowHideButtonControl.xaml
/// </summary>
public partial class ShowHideButtonControl : UserControl
{
	public static readonly DependencyProperty HeaderProperty =
		DependencyProperty.Register
		(
			"Header",
			typeof(string),
			typeof(ShowHideButtonControl),
			new PropertyMetadata("ContentHeader")
		);

	public static readonly DependencyProperty CommandProperty =
		DependencyProperty.Register
		(
			"Command",
			typeof(ICommand),
			typeof(ShowHideButtonControl)
		);

	public static readonly DependencyProperty OrientationProperty =
		DependencyProperty.Register
		(
			"Orientation",
			typeof(OrientationType),
			typeof(ShowHideButtonControl),
			new PropertyMetadata(OrientationType.Vertical)
		);

	public static readonly DependencyProperty DirectionProperty =
		DependencyProperty.Register
		(
			"Direction",
			typeof(ExpandDirection),
			typeof(ShowHideButtonControl),
			new PropertyMetadata(ExpandDirection.Left)
		);

	public static readonly DependencyProperty IsOpenProperty =
		DependencyProperty.Register
		(
			"IsOpen",
			typeof(bool),
			typeof(ShowHideButtonControl),
			new PropertyMetadata(false)
		);

	public string Header
	{
		get => (string)GetValue(HeaderProperty);
		set => SetValue(HeaderProperty, value);
	}

	public ICommand Command
	{
		get => (ICommand)GetValue(CommandProperty);
		set => SetValue(CommandProperty, value);
	}

	public OrientationType Orientation
	{
		get => (OrientationType)GetValue(OrientationProperty);
		set => SetValue(OrientationProperty, value);
	}

	public ExpandDirection Direction
	{
		get => (ExpandDirection)GetValue(DirectionProperty);
		set => SetValue(DirectionProperty, value);
	}

	public bool IsOpen
	{
		get => (bool)GetValue(IsOpenProperty);
		set => SetValue(IsOpenProperty, value);
	}

	public ShowHideButtonControl()
	{
		InitializeComponent();
	}
}
