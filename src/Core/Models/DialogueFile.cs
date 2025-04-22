namespace DialogueEditor.Core.Models;

public class DialogueFile : IEquatable<DialogueFile>
{
	private readonly string _systemPath;

	private Guid _id;
	private DateTime _lastModified;

	public string SystemPath => _systemPath;
	public Guid Id => _id;  // TODO rewrite equatable
	public DateTime LastModified => _lastModified;

	private DialogueFile(string systemPath, DateTime lastModified, Guid? id = null)
	{
		_systemPath = systemPath;
		_id = id ?? Guid.NewGuid();
		_lastModified = lastModified;
	}

	public static DialogueFile Create(string systemPath, DateTime lastModified, Guid? id = null)
		=> new DialogueFile(systemPath, lastModified, id);

	#region Overrides

	public override int GetHashCode()
		=> SystemPath.GetHashCode();

	public override bool Equals(object? obj)
		=> Equals(obj as DialogueFile);

	public static bool operator ==(DialogueFile? left, DialogueFile? right)
		=> (left is null && right is null) || (left is not null && right is not null && left.SystemPath == right.SystemPath);

	public static bool operator !=(DialogueFile? left, DialogueFile? right)
		=> !(left == right);

	#endregion

	#region IEquatable

	public bool Equals(DialogueFile? other)
		=> this == other;

	#endregion
}
