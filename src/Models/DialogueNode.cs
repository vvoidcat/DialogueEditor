namespace DialogueEditor.Models;

public class DialogueNode : IEquatable<DialogueNode>
{
	private readonly Guid _id;
	public Guid Id => _id;

	public NodeContentSettings ContentSettings { get; set; } = new();

	public bool IsRoot { get; set; } = false;
	public bool IsPlayer { get; set; } = false;
	public bool IsLink { get; set; } = false;
	public DialogueNode? Parent { get; set; }
	public DialogueNode? LinkTo { get; set; }

	public List<DialogueNode> Children { get; set; } = new();  // needs Ids only?


	private DialogueNode()
	{
		_id = Guid.NewGuid();
	}

	private DialogueNode(NodeContentSettings contentSettings, IEnumerable<DialogueNode> children) :
		this()
	{
		ContentSettings = contentSettings;
		Children = new List<DialogueNode>();

		foreach (var child in children)
		{
			Children.Add(child);
		}
	}

	private DialogueNode(bool isRoot, bool isPlayer, bool isLink, NodeContentSettings contentSettings, IEnumerable<DialogueNode> children) :
		this(contentSettings, children)
	{
		IsRoot = isRoot;
		IsPlayer = isPlayer;
		IsLink = isLink;
	}

	public static DialogueNode CreateRootNode(IEnumerable<DialogueNode> children)
		=> new DialogueNode(isRoot: true, isPlayer: false, isLink: false, new NodeContentSettings(), children);

	public static DialogueNode CreatePlayerNode(string? content, IEnumerable<DialogueNode> children, bool isLink = false)
		=> new DialogueNode(isRoot: false, isPlayer: true, isLink, new NodeContentSettings(content, "PLAYER"), children);

	public static DialogueNode CreateNpcNode(string? content, string speakerTag, IEnumerable<DialogueNode> children, bool isLink = false)
		=> new DialogueNode(isRoot: false, isPlayer: false, isLink, new NodeContentSettings(content, speakerTag), children);


	public override int GetHashCode()
		=> Id.GetHashCode();

	public override bool Equals(object? obj)
		=> Equals(obj as DialogueNode);

	#region IEquatable

	public bool Equals(DialogueNode? other)
		=> other is not null && Id == other.Id;

	#endregion
}

