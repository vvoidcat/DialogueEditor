using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueEditor.Models;

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

	public override int GetHashCode()
	=> base.GetHashCode();

	public override bool Equals(object? obj)
		=> Equals(obj as NodeContentSettings);

	#region IEquatable

	public bool Equals(NodeContentSettings? other)
		=> other is not null && this.Text == other.Text && this.SpeakerTag == other.SpeakerTag;

	#endregion
}
