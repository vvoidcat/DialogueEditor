using System.Collections.ObjectModel;
using System.Windows.Input;
using DialogueEditor.Core.Models;
using DialogueEditor.ViewModels.Common;

namespace DialogueEditor.ViewModels.ModelWrappers;

public class DialogueWrapper : ObservableObject, IDisposable
{
	private readonly Dialogue _model;

	#region Model Properties

	public Guid Id
	{
		get => _model.Id;
	}

	public string DisplayName
	{
		get => _model.Name;
		set
		{
			if (_model.Name != value)
			{
				_model.Name = value;
				OnPropertyChanged(nameof(DisplayName));
			}
		}
	}

	private DialogueNodeWrapper? _rootNode;
	public DialogueNodeWrapper? RootNode
	{
		get => _rootNode;
		set => SetProperty(ref _rootNode, value, nameof(RootNode));
	}

	private ObservableCollection<DialogueNodeWrapper> _nodes = new();
	public ObservableCollection<DialogueNodeWrapper> Nodes
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

	public delegate void DialogueSelectedNodeChangedHandler(DialogueNodeWrapper? item);
	public event DialogueSelectedNodeChangedHandler OnSelectedNodeChanged = delegate { };

	private DialogueNodeWrapper? _selectedNode;
	public DialogueNodeWrapper? SelectedNode
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

	public delegate void DialogueClosedHandler(DialogueWrapper item);
	public event DialogueClosedHandler OnClosed = delegate { };

	public ICommand CloseCommand => new RelayCommand<object>(() => OnClosed(this));

	#endregion

	#region Constructors

	public DialogueWrapper(Dialogue model)
	{
		_model = model;

		RootNode = new DialogueNodeWrapper(_model.RootNode);
		Nodes = new ObservableCollection<DialogueNodeWrapper>() { RootNode };

		foreach (var node in GetFlatNodesList(Nodes))
		{
			SubscribeToNodeEvents(node);
		}

	}

	#endregion

	#region Private Methods

	private bool ContainsNode(DialogueNodeWrapper? node)
		=> node is not null && _model.AllNodes.ToList().Contains(node.Model);

	private IEnumerable<DialogueNodeWrapper> GetFlatNodesList(IEnumerable<DialogueNodeWrapper> nodes)
	{
		foreach (var node in nodes)
		{
			yield return node;

			foreach (var child in GetFlatNodesList(node.Children))
			{
				yield return child;
			}
		}
	}

	private void DialogueNodeSelected(DialogueNodeWrapper? node)
	{
		if (this.ContainsNode(node))
		{
			SelectedNode = node;
		}
	}

	#endregion

	#region Event Subscribtion Handling

	private void SubscribeToNodeEvents(DialogueNodeWrapper node)
	{
		node.OnIsSelectedChanged += DialogueNodeSelected;
	}

	private void UnsubscribeFromNodeEvents(DialogueNodeWrapper node)
	{
		node.OnIsSelectedChanged -= DialogueNodeSelected;
	}

	#endregion

	#region IDisposable

	public void Dispose()
	{
		foreach (var node in GetFlatNodesList(Nodes))
		{
			UnsubscribeFromNodeEvents(node);
		}
	}

	#endregion
}
