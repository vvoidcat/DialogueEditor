namespace DialogueEditor.Core.Models;

public class Dialogue : IEquatable<Dialogue>
{
	private Guid _id;
	private HashSet<DialogueNode> _allNodes = new();

	public Guid Id => _id;
	public IEnumerable<DialogueNode> AllNodes => _allNodes;

	public string Name { get; set; }
	public DialogueNode RootNode { get; set; }

	public Dialogue(Guid id, DialogueNode rootNode, string? dialogueName = null)
	{
		_id = id;
		Name = dialogueName ?? "NewDialogue";
		RootNode = rootNode;
		BuildNodeSet();
	}

	// TEMP
	public static Dialogue CreateTestDialogue()
	{

		var NpcNodes = new List<DialogueNode>()
		{
			DialogueNode.CreateNpcNode
			(
				"I must speak to you.",
				"NPC1",
				new List<DialogueNode>()
				{
					DialogueNode.CreatePlayerNode
					(
						"Yep",
						new List<DialogueNode>()
					),
					DialogueNode.CreatePlayerNode
					(
						"Yep yep",
						new List<DialogueNode>(),
                        //linkTo: DialogueNode.CreatePlayerNode("aboba bebra bebra flex player flex answer flex", new List<DialogueNode>()) // temp
						linkTo: Guid.NewGuid() // temp
                    ),
					DialogueNode.CreateNpcNode
					(
						"Aboba aboba new Aboba Bebra.",
						"NPC1",
						new List<DialogueNode>(),
						//linkTo: DialogueNode.CreateNpcNode("aboba bebra bebra flex", "Aboba", new List<DialogueNode>()) // temp
						linkTo: Guid.NewGuid() // temp
					)
				}
			),
			DialogueNode.CreateNpcNode
			(
				"I must speak to you again.",
				"NPC1",
				new List<DialogueNode>()
			),
			DialogueNode.CreateNpcNode
			(
				"Aboba aboba.",
				"NPC1",
				new List<DialogueNode>()
			)
		};

		var root = DialogueNode.CreateRootNode(NpcNodes);

		return new Dialogue(Guid.NewGuid(), root, "TEST dialogue");
	}

	public static Dialogue CreateNewDialogue(Guid id, string? dialogueName = null)
		=> new Dialogue(id, DialogueNode.CreateRootNode(new List<DialogueNode>() { DialogueNode.CreateNpcNode() }));

	private void AddNodeRecursive(DialogueNode node)
	{
		_allNodes.Add(node);

		foreach (var child in node.Children)
		{
			AddNodeRecursive(child);
		}
	}

	private void BuildNodeSet()
	{
		_allNodes.Clear();

		AddNodeRecursive(RootNode);
	}

	#region Overrides

	public override int GetHashCode()
		=> Id.GetHashCode();

	public override bool Equals(object? obj)
		=> Equals(obj as Dialogue);

	public static bool operator ==(Dialogue? left, Dialogue? right)
		=> (left is null && right is null) || (left is not null && right is not null && left.Id == right.Id);

	public static bool operator !=(Dialogue? left, Dialogue? right)
		=> !(left == right);

	#endregion

	#region IEquatable

	public bool Equals(Dialogue? other)
		=> this == other;

	#endregion
}
