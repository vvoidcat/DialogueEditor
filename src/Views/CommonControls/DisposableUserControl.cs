using System.Windows.Controls;

namespace DialogueEditor.Views.CommonControls;

public class DisposableUserControl : UserControl
{
	public DisposableUserControl()
	{
		Dispatcher.ShutdownStarted += OnDispatcherShutDownStarted;
	}

	protected void OnDispatcherShutDownStarted(object? sender, EventArgs e)
	{
		var disposable = DataContext as IDisposable;
		if (disposable is not null)
		{
			disposable.Dispose();
		}
	}
}
