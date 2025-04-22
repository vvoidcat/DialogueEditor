namespace DialogueEditor.Core.Models;

public class Condition<T>
{
	public Variable<T> Variable { get; set; }
	public ConditionType ComparisonOperator { get; set; }
	public Variable<T> ExpectedValue { get; set; }
}
