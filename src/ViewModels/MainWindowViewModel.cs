using System.Windows.Input;
using DialogueEditor.ViewModels.Common;

namespace DialogueEditor.ViewModels;

internal class MainWindowViewModel : ObservableObject
{
	private bool _isVisibleConfigsPanel;
	public bool IsVisibleConfigsPanel
	{
		get => _isVisibleConfigsPanel;
		set => SetProperty(ref _isVisibleConfigsPanel, value, nameof(IsVisibleConfigsPanel));
	}

	private bool _isVisibleVariablesPanel;
	public bool IsVisibleVariablesPanel
	{
		get => _isVisibleVariablesPanel;
		set => SetProperty(ref _isVisibleVariablesPanel, value, nameof(IsVisibleVariablesPanel));
	}

	private bool _isVisibleDlgFilesPanel;
	public bool IsVisibleDlgFilesPanel
	{
		get => _isVisibleDlgFilesPanel;
		set => SetProperty(ref _isVisibleDlgFilesPanel, value, nameof(IsVisibleDlgFilesPanel));
	}

	public ICommand toggleHideConfigsCommand => new RelayCommand<object>(() => IsVisibleConfigsPanel = !IsVisibleConfigsPanel);
	public ICommand toggleHideVariablesCommand => new RelayCommand<object>(() => IsVisibleVariablesPanel = !IsVisibleVariablesPanel);
	public ICommand toggleHideDlgFilesCommand => new RelayCommand<object>(() => IsVisibleDlgFilesPanel = !IsVisibleDlgFilesPanel);

	public MainWindowViewModel()
	{
	}
}
