using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueEditor.Models;

public class Variable<T>
{
	public Guid Id { get; set; }
	public string Name { get; set; } = "Name";
	public VariableType Type { get; set; }
	public T Value { get; set; }
}
