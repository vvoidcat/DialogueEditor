namespace DialogueEditor.Core.Models;

public class NodeContentSettings : IEquatable<NodeContentSettings>
{
	public string? Text { get; set; }
	public string? SpeakerTag { get; set; }

	public NodeContentSettings() { }

	public NodeContentSettings(string? content = null, string? speakerTag = null)
	{
		Text = content;
		SpeakerTag = speakerTag;
	}

	#region Overrides

	public override int GetHashCode()
		=> base.GetHashCode();

	public override bool Equals(object? obj)
		=> Equals(obj as NodeContentSettings);

	public static bool operator ==(NodeContentSettings? left, NodeContentSettings? right)
		=> (left is null && right is null) || (left is not null && right is not null &&
			left.Text == right.Text && left.SpeakerTag == right.SpeakerTag);

	public static bool operator !=(NodeContentSettings? left, NodeContentSettings? right)
		=> !(left == right);

	#endregion

	#region IEquatable

	public bool Equals(NodeContentSettings? other)
		=> this == other;

	#endregion
}
