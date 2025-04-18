using System.Windows.Input;
using DialogueEditor.Controllers.Common;

namespace DialogueEditor.Controllers;

internal class MainWindowViewModel : ObservableObject
{
    public int MinResizeWidth { get; set; } = 200;

    private bool _isVisibleConfigsPanel;
    public bool IsVisibleConfigsPanel
    {
        set
        {
            _isVisibleConfigsPanel = value;
            OnPropertyChanged(nameof(IsVisibleConfigsPanel));
        }
        get => _isVisibleConfigsPanel;
    }

    private bool _isVisibleVariablesPanel;
    public bool IsVisibleVariablesPanel
    {
        set
        {
            _isVisibleVariablesPanel = value;
            OnPropertyChanged(nameof(IsVisibleVariablesPanel));
        }
        get => _isVisibleVariablesPanel;
    }

    private bool _isVisibleDlgFilesPanel;
    public bool IsVisibleDlgFilesPanel
    {
        set
        {
            _isVisibleDlgFilesPanel = value;
            OnPropertyChanged(nameof(IsVisibleDlgFilesPanel));
        }
        get => _isVisibleDlgFilesPanel;
    }


    public ICommand toggleHideConfigsCommand => new RelayCommand<object>(() => IsVisibleConfigsPanel = !IsVisibleConfigsPanel);
    public ICommand toggleHideVariablesCommand => new RelayCommand<object>(() => IsVisibleVariablesPanel = !IsVisibleVariablesPanel);
    public ICommand toggleHideDlgFilesCommand => new RelayCommand<object>(() => IsVisibleDlgFilesPanel = !IsVisibleDlgFilesPanel);

    public MainWindowViewModel()
    {
    }
}
