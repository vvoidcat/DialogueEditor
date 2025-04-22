using System.Collections.ObjectModel;
using DialogueEditor.Core.Extensions;
using DialogueEditor.Core.Models;
using DialogueEditor.ViewModels.Common;

namespace DialogueEditor.ViewModels.ModelWrappers;

public class DialogueNodeWrapper : ObservableObject
{
	private readonly DialogueNode _model;
	public DialogueNode Model => _model;

	#region Model Properties

	public bool IsRoot => _model.IsRoot;
	public bool IsPlayer => _model.NodeType == NodeType.Player;
	public bool IsLink => _model.LinkTo is not null;

	private string? _text;
	public string? Text
	{
		get => _text;
		set
		{
			if (SetProperty(ref _text, value, nameof(Text)))
			{
				OnPropertyChanged(nameof(DisplayName));
			}
		}
	}

	private string? _speakerTag;
	public string? SpeakerTag
	{
		get => _speakerTag;
		set
		{
			if (SetProperty(ref _speakerTag, value, nameof(SpeakerTag)))
			{
				OnPropertyChanged(nameof(DisplayName));
			}
		}
	}

	private ObservableCollection<DialogueNodeWrapper> _children = new();
	public ObservableCollection<DialogueNodeWrapper> Children
	{
		get => _children;
		set
		{
			_children = value;
			OnPropertyChanged(nameof(Children));
		}
	}

	#endregion

	#region ViewModel Properties

	public string DisplayName
	{
		//get => _model.ContentSettings is null ? "+" : IsRoot ? "ROOT" :
		//    $"[{_model.ContentSettings.SpeakerTag}] {_model.ContentSettings.Text.TruncateAndAdd(200, "...")}";

		// should be direct and depend on saved changes
		get => _model.ContentSettings is null ? "+" : IsRoot ? "[ROOT]" :
			(string.IsNullOrWhiteSpace(SpeakerTag) ? "" : $"[{SpeakerTag}] ") +
			(string.IsNullOrWhiteSpace(Text) ? "[CONTINUE...]" : $"{Text.TruncateAndAdd(200, "...")}");
	}

	public delegate void DialogueNodeSelectedHandler(DialogueNodeWrapper? item);
	public event DialogueNodeSelectedHandler OnIsSelectedChanged = delegate { };

	private bool _isSelectedNode;
	public bool IsSelectedNode
	{
		get => _isSelectedNode;
		set
		{
			if (SetProperty(ref _isSelectedNode, value, nameof(IsSelectedNode)))
			{
				OnIsSelectedChanged(this);
			}
		}
	}

	#endregion

	#region Constructors

	public DialogueNodeWrapper(DialogueNode model)
	{
		_model = model;

		Children = new ObservableCollection<DialogueNodeWrapper>(BuildChildren(model));

		Text = model.ContentSettings.Text;
		SpeakerTag = model.ContentSettings.SpeakerTag;
	}

	#endregion

	#region Private Methods

	private IEnumerable<DialogueNodeWrapper> BuildChildren(DialogueNode node)
	{
		foreach (var child in node.Children)
		{
			yield return new DialogueNodeWrapper((DialogueNode)child);
		}
	}

	#endregion
}
