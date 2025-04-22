namespace DialogueEditor.Core.Models;

public class DialogueNode : IEquatable<DialogueNode>
{
	private readonly Guid _id;
	public Guid Id => _id;

	public NodeContentSettings ContentSettings { get; set; } = new();

	public bool IsRoot { get; set; } = false;
	public Guid? Parent { get; set; }
	public Guid? LinkTo { get; set; }

	public NodeType NodeType { get; set; }

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

	private DialogueNode(bool isRoot, NodeType nodeType, Guid? linkTo, NodeContentSettings contentSettings, IEnumerable<DialogueNode> children) :
		this(contentSettings, children)
	{
		IsRoot = isRoot;
		NodeType = nodeType;
		LinkTo = linkTo;
	}

	public static DialogueNode CreateRootNode(IEnumerable<DialogueNode> children)
		=> new DialogueNode(isRoot: true, NodeType.None, null, new NodeContentSettings(), children);

	public static DialogueNode CreatePlayerNode(string? content, IEnumerable<DialogueNode> children, Guid? linkTo = null)
		=> new DialogueNode(isRoot: false, NodeType.Player, linkTo, new NodeContentSettings(content, "PLAYER"), children);

	public static DialogueNode CreateNpcNode(string? content = null, string? speakerTag = null, IEnumerable<DialogueNode>? children = null, Guid? linkTo = null)
		=> new DialogueNode(isRoot: false, NodeType.Other, linkTo, new NodeContentSettings(content, speakerTag), children ?? new List<DialogueNode>());

	// create link node

	#region Overrides

	public override int GetHashCode()
		=> Id.GetHashCode();

	public override bool Equals(object? obj)
		=> Equals(obj as DialogueNode);

	public static bool operator ==(DialogueNode? left, DialogueNode? right)
		=> (left is null && right is null) || (left is not null && right is not null && left.Id == right.Id);

	public static bool operator !=(DialogueNode? left, DialogueNode? right)
		=> !(left == right);

	#endregion

	#region IEquatable

	public bool Equals(DialogueNode? other)
		=> this == other;

	#endregion
}

