using System.Windows.Input;
using DialogueEditor.Controllers.Common;

namespace DialogueEditor.Controllers;

internal class MainWindowViewModel : ObservableObject
{
    public int MinResizeWidth { get; set; } = 200;

    private bool _isVisibleConfigsPanel;
    public bool isVisibleConfigsPanel
    {
        set
        {
            _isVisibleConfigsPanel = value;
            OnPropertyChanged(nameof(isVisibleConfigsPanel));
        }
        get => _isVisibleConfigsPanel;
    }

    private bool _isVisibleVariablesPanel;
    public bool isVisibleVariablesPanel
    {
        set
        {
            _isVisibleVariablesPanel = value;
            OnPropertyChanged(nameof(isVisibleVariablesPanel));
        }
        get => _isVisibleVariablesPanel;
    }

    private bool _isVisibleDlgFilesPanel;
    public bool isVisibleDlgFilesPanel
    {
        set
        {
            _isVisibleDlgFilesPanel = value;
            OnPropertyChanged(nameof(isVisibleDlgFilesPanel));
        }
        get => _isVisibleDlgFilesPanel;
    }


    public ICommand toggleHideConfigsCommand => new RelayCommand<object>(() => isVisibleConfigsPanel = !isVisibleConfigsPanel);
    public ICommand toggleHideVariablesCommand => new RelayCommand<object>(() => isVisibleVariablesPanel = !isVisibleVariablesPanel);
    public ICommand toggleHideDlgFilesCommand => new RelayCommand<object>(() => isVisibleDlgFilesPanel = !isVisibleDlgFilesPanel);

    public MainWindowViewModel()
    {
    }
}
