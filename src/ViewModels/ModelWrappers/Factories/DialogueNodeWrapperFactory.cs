using DialogueEditor.Core.Models;
using DialogueEditor.ViewModels.ModelWrappers.Factories.Interfaces;

namespace DialogueEditor.ViewModels.ModelWrappers.Factories;

public class DialogueNodeWrapperFactory : IModelWrapperFactory<DialogueNodeWrapper, DialogueNode>
{
	public DialogueNodeWrapper Create(DialogueNode model)
		=> new DialogueNodeWrapper(model);
}
