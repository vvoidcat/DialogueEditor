using System.Collections.ObjectModel;
using DialogueEditor.Extensions;
using DialogueEditor.Models;
using DialogueEditor.ViewModels.Common;

namespace DialogueEditor.ViewModels.ModelWrappers;

public class DialogueNodeViewModel : ObservableObject
{
	private readonly DialogueNode _model;

	#region Model Properties

	public bool IsRoot => _model.IsRoot;
	public bool IsPlayer => _model.IsPlayer;
	public bool IsLink => _model.IsLink;

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

	public ObservableCollection<DialogueNodeViewModel> Children
	{
		get
		{
			var result = new ObservableCollection<DialogueNodeViewModel>();

			foreach (var child in _model.Children)
			{
				result.Add(new DialogueNodeViewModel(child));
			}

			return result;
		}
	}

	#endregion

	#region ViewModel Properties

	public string DisplayName
	{
		//get => _model.ContentSettings is null ? "+" : IsRoot ? "ROOT" :
		//    $"[{_model.ContentSettings.SpeakerTag}] {_model.ContentSettings.Text.TruncateAndAdd(200, "...")}";

		// should be direct and depend on saved changes
		get => _model.ContentSettings is null ? "+" : IsRoot ? "ROOT" :
			$"[{SpeakerTag}] {Text.TruncateAndAdd(200, "...")}";
	}

	public delegate void DialogueNodeSelectedHandler(object item);
	public static event DialogueNodeSelectedHandler OnIsSelectedChanged = delegate { };

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

	public DialogueNodeViewModel(DialogueNode model)
	{
		_model = model;

		Text = model.ContentSettings.Text;
		SpeakerTag = model.ContentSettings.SpeakerTag;
	}
}
