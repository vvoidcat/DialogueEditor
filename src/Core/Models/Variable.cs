namespace DialogueEditor.Core.Models;

public class Variable<T>
{
	//public Guid Id { get; set; }
	public string Name { get; set; } = "Name";
	public VariableType Type { get; set; }
	public T Value { get; set; }

	//public Variable( )
	//{

	//}
}
