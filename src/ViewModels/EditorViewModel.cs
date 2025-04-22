using System.Collections.ObjectModel;
using System.Windows.Input;
using DialogueEditor.ViewModels.Common;
using DialogueEditor.ViewModels.ModelWrappers;
using DialogueEditor.ViewModels.ModelWrappers.Factories;

namespace DialogueEditor.ViewModels;

public class EditorViewModel : ObservableObject, IDisposable
{
	private readonly DialogueWrapperFactory _modelWrapperFactory;

	#region Properties

	private DialogueWrapper? _activeDialogue;
	public DialogueWrapper? ActiveDialogue
	{
		get => _activeDialogue;
		set
		{
			if (SetProperty(ref _activeDialogue, value, nameof(ActiveDialogue)))
			{
				SelectedNodeChanged(ActiveDialogue?.SelectedNode ?? ActiveDialogue?.RootNode);
			}
		}
	}

	private DialogueNodeWrapper? _activeDialogueSelectedNode;
	public DialogueNodeWrapper? ActiveDialogueSelectedNode
	{
		get => _activeDialogueSelectedNode;
		set => SetProperty(ref _activeDialogueSelectedNode, value, nameof(ActiveDialogueSelectedNode));
	}

	private ObservableCollection<DialogueWrapper> _openDialogues = new();
	public ObservableCollection<DialogueWrapper> OpenDialogues
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

	public ICommand toggleHideNodeSettingsCommand
		=> new RelayCommand<object>(() => IsVisibleNodeSettingsPanel = !IsVisibleNodeSettingsPanel);

	#endregion

	#region Constructors

	public EditorViewModel
	(
		DialogueWrapperFactory modelWrapperFactory
	)
	{
		_modelWrapperFactory = modelWrapperFactory;

		var dialogue = _modelWrapperFactory.Create(Core.Models.Dialogue.CreateNewDialogue(Guid.NewGuid()));
		var testDialogue = _modelWrapperFactory.Create(Core.Models.Dialogue.CreateTestDialogue());

		AddDialogue(dialogue);
		AddDialogue(testDialogue);

		SubscribeToDialogueFileEvents();
	}

	#endregion

	#region Private Methods

	private void AddDialogue(DialogueWrapper dialogue)
	{
		OpenDialogues.Add(dialogue);
		SubscribeToDialogueEvents(dialogue);
	}

	private void RemoveDialogue(DialogueWrapper dialogue)
	{
		OpenDialogues.Remove(dialogue);
		UnsubscribeFromDialogueEvents(dialogue);
	}

	private void SelectedNodeChanged(DialogueNodeWrapper? node)
	{
		ActiveDialogueSelectedNode = node;
	}

	private void DialogueFileOpened(object? item)
	{
		if (item is DialogueFileWrapper file)
		{
			var foundAlreadyOpen = OpenDialogues.Where(x => x.Id == file.Id).FirstOrDefault();

			if (foundAlreadyOpen is null)
			{
				var newDialogue = _modelWrapperFactory.Create(Core.Models.Dialogue.CreateNewDialogue(file.Id));
				AddDialogue(newDialogue);
				foundAlreadyOpen = newDialogue;
			}

			ActiveDialogue = foundAlreadyOpen;
		}
	}

	private void DialogueTabClosed(DialogueWrapper dialogue)
	{
		for (int i = 0; i < OpenDialogues.Count; i++)
		{
			if (OpenDialogues[i] == dialogue)
			{
				if (ActiveDialogue == dialogue)
				{
					int? newActiveIndex = OpenDialogues.Count > 1 ? (i == 0 ? i + 1 : i - 1) : null;

					DialogueWrapper newActiveDialogue;

					if (newActiveIndex is null)
					{
						newActiveDialogue = _modelWrapperFactory.Create(Core.Models.Dialogue.CreateNewDialogue(Guid.NewGuid()));
						OpenDialogues.Add(newActiveDialogue);
					}
					else
					{
						newActiveDialogue = OpenDialogues[(int)newActiveIndex];
					}

					ActiveDialogue = newActiveDialogue;
				}

				RemoveDialogue(dialogue);
				break;
			}
		}
	}

	#endregion

	#region Event Subscribtion Handling

	private void SubscribeToDialogueEvents(DialogueWrapper dialogue)
	{
		dialogue.OnSelectedNodeChanged += SelectedNodeChanged;
		dialogue.OnClosed += DialogueTabClosed;
	}

	private void SubscribeToDialogueFileEvents()
	{
		DialogueFilesViewModel.OnDialogueFileOpened += DialogueFileOpened;
	}

	private void UnsubscribeFromDialogueEvents(DialogueWrapper dialogue)
	{
		dialogue.OnSelectedNodeChanged -= SelectedNodeChanged;
		dialogue.OnClosed -= DialogueTabClosed;
		dialogue.Dispose();
	}

	private void UnsubscribeFromDialogueFileEvents()
	{
		DialogueFilesViewModel.OnDialogueFileOpened -= DialogueFileOpened;
	}

	#endregion

	#region IDisposable

	public void Dispose()
	{
		UnsubscribeFromDialogueFileEvents();

		foreach (var dialogue in OpenDialogues)
		{
			UnsubscribeFromDialogueEvents(dialogue);
		}
	}

	#endregion
}
