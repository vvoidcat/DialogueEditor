using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueEditor.Models;

public enum ConditionType
{
	EQUAL,
	NOT_EQUAL,
	GREATER_THAN,
	LESS_THAN,
	GREATER_OR_EQUAL,
	LESS_OR_EQUAL,
	CONTAINS,
	STARTS_WITH,
	ENDS_WITH,
	IS_NULL,
	IS_NOT_NULL
}
