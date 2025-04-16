using System.Collections.ObjectModel;
using System.Windows.Input;
using DialogueEditor.Models;
using DialogueEditor.ViewModels.Common;
using DialogueEditor.ViewModels.ModelWrappers;

namespace DialogueEditor.ViewModels;

class EditorPanelViewModel : ObservableObject, IDisposable
{
	private DialogueViewModel? _selectedDialogue;
	public DialogueViewModel? SelectedDialogue
	{
		get => _selectedDialogue;
		set
		{
			if (SetProperty(ref _selectedDialogue, value, nameof(SelectedDialogue)))
			{
				OnPropertyChanged(nameof(ActiveDialogueSelectedNode));
			}
		}
	}

	private DialogueNodeViewModel? _activeDialogueSelectedNode;
	public DialogueNodeViewModel? ActiveDialogueSelectedNode
	{
		get => _activeDialogueSelectedNode;
		set
		{
			SetProperty(ref _activeDialogueSelectedNode, value, nameof(ActiveDialogueSelectedNode));
		}
	}

	private ObservableCollection<DialogueViewModel> _openDialogues = new();
	public ObservableCollection<DialogueViewModel> OpenDialogues
	{
		get => _openDialogues;
		set
		{
			_openDialogues = value;
			OnPropertyChanged(nameof(OpenDialogues));
		}
	}

	private bool _isVisibleNodeSettingsPanel;
	public bool IsVisibleNodeSettingsPanel
	{
		get => _isVisibleNodeSettingsPanel;
		set => SetProperty(ref _isVisibleNodeSettingsPanel, value, nameof(IsVisibleNodeSettingsPanel));
	}

	public ICommand toggleHideNodeSettingsCommand => new RelayCommand<object>(() => IsVisibleNodeSettingsPanel = !IsVisibleNodeSettingsPanel);

	public EditorPanelViewModel()
	{
		var dialogue = new DialogueViewModel(new Dialogue());

		OpenDialogues = new ObservableCollection<DialogueViewModel>()
		{
			dialogue
		};

		SubscribeToDialogueEvents();
	}

	private void SelectedNodeChanged(object? item)
	{
		if (item is DialogueNodeViewModel node)
		{
			// change to SelectedDialogue
			var activeDlg = OpenDialogues.FirstOrDefault();
			var selected = activeDlg?.SelectedNode;

			ActiveDialogueSelectedNode = selected;
		}
	}

	#region Event Handling

	private void SubscribeToDialogueEvents()
	{
		DialogueViewModel.OnSelectedNodeChanged += SelectedNodeChanged;
	}

	private void UnsubscribeFromDialogueEvents()
	{
		DialogueViewModel.OnSelectedNodeChanged -= SelectedNodeChanged;
	}

	#endregion

	#region IDisposable

	public void Dispose()
	{
		SelectedDialogue?.Dispose();

		UnsubscribeFromDialogueEvents();

		foreach (var dialogue in OpenDialogues)
		{
			dialogue.Dispose();
		}
	}

	#endregion
}
