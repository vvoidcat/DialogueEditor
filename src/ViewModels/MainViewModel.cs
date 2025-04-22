using System.Windows.Input;
using DialogueEditor.ViewModels.Common;

namespace DialogueEditor.ViewModels;

public class MainViewModel : ObservableObject
{
	#region Properties

	private bool _isVisibleConfigsPanel = false;
	public bool IsVisibleConfigsPanel
	{
		get => _isVisibleConfigsPanel;
		set => SetProperty(ref _isVisibleConfigsPanel, value, nameof(IsVisibleConfigsPanel));
	}
	public ICommand ToggleHideConfigsCommand
		=> new RelayCommand<object>(() => IsVisibleConfigsPanel = !IsVisibleConfigsPanel);


	private bool _isVisibleVariablesPanel = false;
	public bool IsVisibleVariablesPanel
	{
		get => _isVisibleVariablesPanel;
		set => SetProperty(ref _isVisibleVariablesPanel, value, nameof(IsVisibleVariablesPanel));
	}
	public ICommand ToggleHideVariablesCommand
		=> new RelayCommand<object>(() => IsVisibleVariablesPanel = !IsVisibleVariablesPanel);


	private bool _isVisibleDlgFilesPanel = false;
	public bool IsVisibleDlgFilesPanel
	{
		get => _isVisibleDlgFilesPanel;
		set => SetProperty(ref _isVisibleDlgFilesPanel, value, nameof(IsVisibleDlgFilesPanel));
	}
	public ICommand ToggleHideDlgFilesCommand
		=> new RelayCommand<object>(() => IsVisibleDlgFilesPanel = !IsVisibleDlgFilesPanel);


	public event EventHandler OnRequestHide = delegate { };
	public ICommand HideWindowCommand
		=> new RelayCommand<object>(() => OnRequestHide(this, new EventArgs()));

	public event EventHandler OnRequestResize = delegate { };
	public ICommand ResizeWindowCommand
		=> new RelayCommand<object>(() => OnRequestResize(this, new EventArgs()));

	public event EventHandler OnRequestClose = delegate { };
	public ICommand CloseAppCommand
		=> new RelayCommand<object>(() => OnRequestClose(this, new EventArgs()));

	#endregion

	#region Constructors

	public MainViewModel()
	{
	}

	#endregion
}
