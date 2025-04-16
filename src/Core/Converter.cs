using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueEditor.Core;

internal class Converter : IConverter
{
	// processes single option
	public R ProcessOption<T, R>(T option)
	{
		//placeholder
		return Activator.CreateInstance<R>();
	}
}
