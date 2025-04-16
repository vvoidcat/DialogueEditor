using System.Collections.ObjectModel;
using DialogueEditor.Models;
using DialogueEditor.ViewModels.Common;

namespace DialogueEditor.ViewModels.ModelWrappers;


public class DialogueViewModel : ObservableObject, IDisposable
{
	private readonly Dialogue _model;

	#region Model Properties
	public string Name
	{
		get => _model.Name;
		set
		{
			if (_model.Name != value)
			{
				_model.Name = value;
				OnPropertyChanged(nameof(Name));
			}
		}
	}

	private DialogueNodeViewModel? _rootNode;
	public DialogueNodeViewModel? RootNode
	{
		get => _rootNode;
		set => SetProperty(ref _rootNode, value, nameof(RootNode));
	}

	private ObservableCollection<DialogueNodeViewModel> _nodes = new();
	public ObservableCollection<DialogueNodeViewModel> Nodes
	{
		get => _nodes;
		set
		{
			_nodes = value;
			OnPropertyChanged(nameof(Nodes));
		}
	}

	#endregion

	#region ViewModel Properties

	public delegate void DialogueSelectedNodeChangedHandler(object? item);
	public static event DialogueSelectedNodeChangedHandler OnSelectedNodeChanged = delegate { };

	private DialogueNodeViewModel? _selectedNode;
	public DialogueNodeViewModel? SelectedNode
	{
		get => _selectedNode;
		set
		{
			if (SetProperty(ref _selectedNode, value, nameof(SelectedNode)))
			{
				OnSelectedNodeChanged(SelectedNode);
			}
		}
	}

	#endregion

	public DialogueViewModel(Dialogue model)
	{
		_model = model;

		RootNode = new DialogueNodeViewModel(_model.RootNode);
		Nodes = new ObservableCollection<DialogueNodeViewModel>() { RootNode };

		SubscribeToNodeEvents();
	}

	private IEnumerable<DialogueNodeViewModel> GetFlatNodesList(DialogueNodeViewModel rootNode)
	{
		yield return rootNode;

		foreach (var child in rootNode.Children)
		{
			foreach (var node in GetFlatNodesList(child))
			{
				yield return node;
			}
		}
	}

	private void DialogueNodeSelected(object item)
	{
		if (item is DialogueNodeViewModel node)
		{
			SelectedNode = node;
		}
	}

	#region Event Handling

	private void SubscribeToNodeEvents()
	{
		DialogueNodeViewModel.OnIsSelectedChanged += DialogueNodeSelected;
	}

	private void UnsubscribeFromNodeEvents()
	{
		DialogueNodeViewModel.OnIsSelectedChanged -= DialogueNodeSelected;
	}

	#endregion

	#region IDisposable

	public void Dispose()
	{
		UnsubscribeFromNodeEvents();
	}

	#endregion
}
