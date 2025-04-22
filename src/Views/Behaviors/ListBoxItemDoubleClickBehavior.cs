using System.Windows.Input;
using System.Windows;

namespace DialogueEditor.Views.Behaviors;

public static class ListBoxItemDoubleClickBehavior
{
	public static readonly DependencyProperty CommandProperty =
		DependencyProperty.RegisterAttached
		(
			"Command",
			typeof(ICommand),
			typeof(ListBoxItemDoubleClickBehavior),
			new PropertyMetadata(null, OnCommandChanged)
		);

	public static ICommand GetCommand(DependencyObject obj) => (ICommand)obj.GetValue(CommandProperty);
	public static void SetCommand(DependencyObject obj, ICommand value) => obj.SetValue(CommandProperty, value);

	private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is UIElement element)
		{
			if (e.NewValue is not null)
				element.PreviewMouseLeftButtonDown += Element_PreviewMouseLeftButtonDown;
			else
				element.PreviewMouseLeftButtonDown -= Element_PreviewMouseLeftButtonDown;
		}
	}

	private static void Element_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		if (e.ClickCount == 2 && sender is FrameworkElement element)
		{
			var command = GetCommand(element);
			if (command?.CanExecute(element.DataContext) == true)
			{
				command.Execute(element.DataContext);
				e.Handled = true;
			}
		}
	}
}
