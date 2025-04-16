namespace DialogueEditor.Models;

public class Dialogue
{
	private readonly Guid _id;
	private readonly HashSet<DialogueNode> _allNodes = new();

	public Guid Id => _id;
	public string Name { get; set; } = "DialogueName";
	public string FileExtension { get; set; } = ".json";
	public DialogueNode RootNode { get; set; } = DialogueNode.CreateRootNode(new List<DialogueNode>());
	public IEnumerable<DialogueNode> AllNodes => _allNodes;


	public Dialogue()
	{
		_id = Guid.NewGuid();

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
						isLink: true
					),
					DialogueNode.CreateNpcNode
					(
						"Aboba aboba new Aboba Bebra.",
						"NPC1",
						new List<DialogueNode>(),
						isLink: true
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

		RootNode = root;

		BuildNodeSet();
	}

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
}
