using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueEditor.Models;

public class Condition<T>
{
	public Variable<T> Variable { get; set; }
	public ConditionType ComparisonOperator { get; set; }
	public Variable<T> ExpectedValue { get; set; }
}
